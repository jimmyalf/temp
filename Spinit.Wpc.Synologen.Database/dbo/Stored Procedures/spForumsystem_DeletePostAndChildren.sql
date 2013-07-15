CREATE procedure spForumsystem_DeletePostAndChildren
(
    @PostID int,
    @RootPostID int = null,
    @DeleteChildren bit = 1
)
AS

-- Posts are not "deleted", they are moved to ForumID=4.

DECLARE @ThreadID INT
DECLARE @OldThreadID INT
DECLARE @UserID INT
DECLARE @PostAuthor NVARCHAR(64)
DECLARE @ForumID INT
DECLARE @ParentID INT
DECLARE @IsApproved BIT
DECLARE @MostRecentPostAuthor NVARCHAR(64)
DECLARE @MostRecentPostAuthorID NVARCHAR(64)
DECLARE @MostRecentPostID INT
DECLARE @ForumPostStatisticsEnabled BIT
DECLARE @PostDate DATETIME
DECLARE @EmoticonID INT
DECLARE @IsLocked INT
DECLARE @MinPostLevel INT
DECLARE @MinSortOrder INT


-- First, get information about the post that is about to be deleted.
SELECT
	@OldThreadID = ThreadID,
	@UserID = UserID,
	@PostAuthor = PostAuthor,
	@ParentID = ParentID,
	@ForumID = ForumID,
	@IsLocked = IsLocked,
	@IsApproved = IsApproved,
	@PostDate = PostDate,
	@EmoticonID = EmoticonID
FROM
	tblForumPosts
WHERE
	PostID = @PostID


IF @IsApproved = 1 -- AND @RootPostID IS NULL
BEGIN
	-- We create a new thread here because we don't
	-- know if we are deleting a reply, a thread starter, or both.
	INSERT tblForumThreads 	
		( ForumID,
		PostDate, 
		UserID, 
		PostAuthor, 
		ThreadDate, 
		MostRecentPostAuthor, 
		MostRecentPostAuthorID, 	
		MostRecentPostID, 
		IsLocked, 
		IsApproved,
		IsSticky, 
		StickyDate, 
		ThreadEmoticonID )
	VALUES
		( 4, 	-- the Deleted Posts forum
		@PostDate, 
		@UserID, 
		@PostAuthor,
		@PostDate,
		@PostAuthor,
		@UserID, 
		@PostID,	-- MostRecentPostID, which we don't know until after post INSERT below.
		@IsLocked,
		@IsApproved,
		0,	-- Downgrade the thread to a non-sticky.
		@PostDate,
		@EmoticonID )

	-- Get the new ThreadID
	SELECT 
		@ThreadID = @@IDENTITY
	FROM
		tblForumThreads

	-- Move the post to the new thread
        UPDATE 
		tblForumPosts 
	SET 
		ForumID = 4,
		ThreadID = @ThreadID,
		ParentID = @PostID,
		SortOrder = 1,
		PostLevel = 1		-- set as the thread starter
	WHERE 
		PostID = @PostID
	
	-- delete all child posts, unless DeleteChildred is set to 0.
	IF @DeleteChildren = 1 
	BEGIN
		UPDATE
			tblForumPosts
		SET
			ForumID = 4,
			ThreadID = @ThreadID,
			PostLevel = 2,
			SortOrder = 2
		WHERE
			ParentID = @PostID

		-- EAD: quick fix because it was reset above
		-- (set all others to 2 for now)
		UPDATE
			tblForumPosts
		SET
			PostLevel = 1,
			SortOrder = 1
		WHERE
			PostID = @PostID
	END
	ELSE BEGIN
		-- Have to fix the non-deleted child posts, if any, for the ParentID.
		-- Setting them to the top-level post.
		UPDATE
			tblForumPosts
		SET
			ParentID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @OldThreadID ORDER BY PostID ASC)
		WHERE
			ThreadID = @OldThreadID
	END
    
	-- update the new thread's stats
	SELECT TOP 1
		@MostRecentPostAuthor = PostAuthor,
		@MostRecentPostAuthorID = UserID,
		@MostRecentPostID = PostID
	FROM
		tblForumPosts
	WHERE
		ThreadID = @ThreadID
	ORDER BY
		PostID DESC

	UPDATE
		tblForumThreads
	SET
		MostRecentPostAuthor = @MostRecentPostAuthor, 
		MostRecentPostAuthorID = @MostRecentPostAuthorID, 	
		MostRecentPostID = @MostRecentPostID
	WHERE
		ThreadID = @ThreadID		


	-- If no posts are linked to the OldthreadID, delete the old thread
	IF NOT EXISTS(SELECT ThreadID FROM tblForumPosts WHERE ThreadID = @OldThreadID)
	BEGIN
		-- Delete all thread tracking data.	
		DELETE FROM 
			tblForumSearchBarrel
		WHERE 
			ThreadID = @OldThreadID

		-- Delete all thread tracking data.	
		DELETE FROM 
			tblForumTrackedThreads
		WHERE 
			ThreadID = @OldThreadID

		-- Delete all thread read data.
		DELETE FROM 
			tblForumThreadsRead
		WHERE 
			ThreadID = @OldThreadID

		-- Delete the thread
		DELETE
			tblForumThreads
		WHERE
			ThreadID = @OldThreadID
	END

	-- Decrease the TotalPosts on the user's profile.
	IF (SELECT EnablePostStatistics FROM tblForumForums WHERE ForumID = @ForumID) = 1
		UPDATE 
			tblForumUserProfile
		SET 
			TotalPosts = ISNULL(TotalPosts - (SELECT COUNT(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID), 0)
		WHERE 
			UserID = @UserID
END

-- Delete from the search index
DELETE 
	tblForumSearchBarrel 
WHERE 
	PostID = @PostID


-- If the post is approved, reset the statistics on the forums and threads table.
IF @IsApproved = 1
BEGIN
	EXEC spForumsystem_ResetThreadStatistics @OldThreadID
	EXEC spForumsystem_ResetThreadStatistics @ThreadID
	EXEC spForumsystem_ResetForumStatistics @ForumID
	EXEC spForumsystem_ResetForumStatistics 4
END


