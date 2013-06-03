CREATE                     PROCEDURE spForumPost_CreateUpdate
(
	@ForumID int,
	@ParentID int,
	@AllowDuplicatePosts bit,
	@DuplicateIntervalInMinutes int = 0,
	@Subject nvarchar(256),
	@UserID int,
	@PostAuthor nvarchar(64) = null,
	@Body ntext,
	@FormattedBody ntext,
	@EmoticonID int = 0,
	@IsLocked bit,
	@IsSticky bit,
	@StickyDate datetime,
	@PostType int = 0,
	@PostDate datetime = null,
	@UserHostAddress nvarchar(32),
	@PostID int out
) 
AS
SET NOCOUNT ON
DECLARE @MaxSortOrder int
DECLARE @ParentLevel int
DECLARE @ThreadID int
DECLARE @ParentSortOrder int
DECLARE @NextSortOrder int
DECLARE @ApprovedPost bit
DECLARE @ModeratedForum bit
DECLARE @EnablePostStatistics bit
DECLARE @TrackThread bit

-- set the PostDate
IF @PostDate IS NULL
	SET @PostDate = GetDate()

-- set the username
IF @PostAuthor IS NULL
	SELECT 
		@PostAuthor = UserName
	FROM 
		tblForumUsers 
	WHERE 
		UserID = @UserID

-- Do we care about duplicates?
IF @AllowDuplicatePosts = 0
BEGIN
	DECLARE @IsDuplicate bit
	exec spForumsystem_DuplicatePost @UserID, @Body, @DuplicateIntervalInMinutes, @IsDuplicate output

	IF @IsDuplicate = 1
	BEGIN
		SET @PostID = -1	
		RETURN	-- Exit with error code.
	END
END

-- we need to get the ForumID, if the ParentID is not null (there should be a ForumID)
IF @ForumID = 0 AND @ParentID <> 0
	SELECT 
		@ForumID = ForumID
	FROM 
		tblForumPosts (nolock) 
	WHERE 
		PostID = @ParentID

-- Is this forum moderated?
SELECT 
	@ModeratedForum = IsModerated, @EnablePostStatistics = EnablePostStatistics
FROM 
	tblForumForums (nolock)
WHERE 
	ForumID = @ForumID

-- Determine if this post will be approved.
-- If the forum is NOT moderated, then the post will be approved by default.
SET NOCOUNT ON
BEGIN TRAN

IF @ModeratedForum = 0 OR @ForumID = 0
	SELECT @ApprovedPost = 1
ELSE
BEGIN
	-- ok, this is a moderated forum.  Is this user trusted?  If he is, then the post is approved ; else it is not
	SET @ApprovedPost = ( 
		SELECT
			ModerationLevel
		FROM
			tblForumUserProfile (nolock)
		WHERE
			UserID = @UserID )
END


-- EAD: Modifying this sproc to insert directly into tblForumThreads.  We are no longer keying
-- tblForumThreads.ThreadID to be same number as the top PostID for the thread.  This is to allow
-- for the FKs to be put into place.

IF @ParentID = 0	-- parameter indicating this is a top-level post (for a new thread)
BEGIN
	-- First we create a new ThreadID.

	-- check the StickyDate to ensure it's not null
	IF @StickyDate < @PostDate
		SET @StickyDate = @PostDate

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
		( @ForumID, 
		@PostDate, 
		@UserID, 
		@PostAuthor,
		@PostDate,
		@PostAuthor,
		@UserID, 
		0,	-- MostRecentPostID, which we don't know until after post INSERT below.
		@IsLocked,
		@ApprovedPost,
		@IsSticky,
		@StickyDate,
		@EmoticonID )

	-- Get the new ThreadID
	SELECT 
		@ThreadID = @@IDENTITY
	FROM
		tblForumThreads
		
	-- Now we add the new post
	INSERT tblForumPosts 
		( ForumID, 
		ThreadID, 
		ParentID, 
		PostLevel, 
		SortOrder, 
		Subject, 
		UserID, 
		PostAuthor, 
		IsApproved, 
		IsLocked, 
		Body, 
		FormattedBody, 
		PostType, 
		PostDate, 
		IPAddress, 
		EmoticonID )
	VALUES 
		( @ForumID, 
		@ThreadID, 
		0, 	-- ParentID, which we don't know until after INSERT
		1, 	-- PostLevel, 1 marks start/top/first post in thread.
		1, 	-- SortOrder (not in use at this time)
		@Subject, 
		@UserID, 
		@PostAuthor,
		@ApprovedPost, 
		@IsLocked, 
		@Body, 
		@FormattedBody, 
		@PostType, 
		@PostDate, 
		@UserHostAddress, 
		@EmoticonID )
		

	-- Get the new PostID
	SELECT 
		@PostID = @@IDENTITY
--	FROM
--		tblForumPosts

	-- Update the new Thread with the new PostID
	UPDATE 
		tblForumThreads
	SET 
		MostRecentPostID = @PostID
	WHERE 
		ThreadID = @ThreadID

	-- Update the new Post's ParentID with the new PostID
	UPDATE 
		tblForumPosts
	SET 
		ParentID = @PostID
	WHERE 
		PostID = @PostID

END
ELSE BEGIN	-- @ParentID <> 0 means there is a reply to an existing post

	-- Get the Post Information for what we are replying to
	SELECT 
		@ThreadID = ThreadID,
		@ForumID = ForumID,
		@ParentLevel = PostLevel,
		@ParentSortOrder = SortOrder
	FROM 
		tblForumPosts
	WHERE 
		PostID = @ParentID

	-- Is there another post at the same level or higher?
	SET @NextSortOrder = (
		SELECT 	
			MIN(SortOrder) 
		FROM 
			tblForumPosts 
		WHERE 
			PostLevel <= @ParentLevel 
			AND SortOrder > @ParentSortOrder 
			AND ThreadID = @ThreadID )

	IF @NextSortOrder > 0
	BEGIN
		-- Move the existing posts down
		UPDATE 
			tblForumPosts
		SET 
			SortOrder = SortOrder + 1
		WHERE 
			ThreadID = @ThreadID
			AND SortOrder >= @NextSortOrder

		SET @MaxSortOrder = @NextSortOrder

	END
   	ELSE BEGIN 	-- There are no posts at this level or above
	
		-- Find the highest sort order for this parent
		SELECT 
			@MaxSortOrder = MAX(SortOrder) + 1
		FROM 
			tblForumPosts
		WHERE 
			ThreadID = @ThreadID

	END 

	-- Insert the new post
	INSERT tblForumPosts 
		( ForumID, 
		ThreadID, 
		ParentID, 
		PostLevel, 
		SortOrder, 
		Subject, 
		UserID, 
		PostAuthor, 
		IsApproved, 
		IsLocked, 
		Body, 
		FormattedBody, 
		PostType, 
		PostDate, 
		IPAddress, 
		EmoticonID )
	VALUES 
		( @ForumID, 
		@ThreadID, 
		@ParentID, 
		@ParentLevel + 1, 
		@MaxSortOrder,
		@Subject, 
		@UserID, 
		@PostAuthor, 
		@ApprovedPost, 
		@IsLocked, 
		@Body, 
		@FormattedBody, 
		@PostType, 
		@PostDate, 
		@UserHostAddress, 
		@EmoticonID )


	-- Now check to see if this post is Approved by default.
	-- If so, we go ahead and update the Threads table for the MostRecent items.
	IF @ApprovedPost = 1
	BEGIN		
		-- Grab the new PostID and update the ThreadID's info
		SELECT 
			@PostID = @@IDENTITY 
--		FROM 
--			tblForumPosts

		-- To cut down on overhead, I've elected to update the thread's info
		-- directly from here, without running spForumsystem_UpdateThread since
		-- I already have all of the information that this sproc would normally have to lookup.
		IF @StickyDate < @PostDate
			SET @StickyDate = @PostDate

		UPDATE
			tblForumThreads 	
		SET 
			MostRecentPostAuthor = @PostAuthor,
			MostRecentPostAuthorID = @UserID,
			MostRecentPostID = @PostID,
			TotalReplies = TotalReplies + 1, -- (SELECT COUNT(*) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
			IsLocked = @IsLocked,
			StickyDate = @StickyDate,	-- this makes the thread a sticky/announcement, even if it's a reply.
			ThreadDate = @PostDate
		WHERE
			ThreadID = @ThreadID
	END
	ELSE
	BEGIN
		-- Moderated Posts: get the new PostID
		SELECT @PostID = @@IDENTITY 
	END

	-- Clean up ThreadsRead (this should work very well now)
	DELETE
		tblForumThreadsRead
	WHERE
		ThreadID = @ThreadID 
		AND UserID != @UserID
END


-- Update the users tracking for the new post (if needed)
SELECT 
	@TrackThread = EnableThreadTracking 
FROM 
	tblForumUserProfile (nolock) 
WHERE 
	UserID = @UserID

IF @TrackThread = 1
	-- If a row already exists to track this thread for this user, do nothing - otherwise add the row
	IF NOT EXISTS ( SELECT ThreadID FROM tblForumTrackedThreads (nolock) WHERE ThreadID = @ThreadID AND UserID = @UserID )
		INSERT INTO tblForumTrackedThreads 
			( ThreadID, 
			UserID )
		VALUES
			( @ThreadID, 
			@UserID )

COMMIT TRAN
BEGIN TRAN

-- Is this a private post
IF @ForumID = 0
	EXEC spForumPrivateMessages_CreateDelete @UserID, @ThreadID, 0

-- Update the forum statitics
IF @ApprovedPost = 1 AND @ForumID > 0 AND @UserID > 0
BEGIN
	IF @EnablePostStatistics = 1
	BEGIN
		UPDATE 
			tblForumUserProfile 
		SET 
			TotalPosts = TotalPosts + 1 
		WHERE 
			UserID = @UserID
	END

	EXEC spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID
END

 -- Clean up unnecessary records in forumsread
-- EXEC spForumsystem_CleanForumsRead @ForumID

COMMIT TRAN
SET NOCOUNT OFF

SELECT @PostID = @PostID



