CREATE   procedure spForumsystem_UpdateThread (
	@ThreadID int,
	@ReplyPostID int
)
AS
BEGIN
SET NOCOUNT ON
DECLARE @ThreadDate datetime
DECLARE @StickyDate datetime
DECLARE @UserID int
DECLARE @PostAuthor nvarchar(64)
DECLARE @FirstPostID INT

IF @ReplyPostID = 0
	SELECT TOP 1 
		@ReplyPostID = PostID 
	FROM 
		tblForumPosts
	WHERE
		ThreadID = @ThreadID
		AND IsApproved = 1
	ORDER BY
		PostDate DESC


-- Get details about the reply
SELECT 
	@ThreadDate = PostDate, 
	@UserID = UserID, 
	@PostAuthor = PostAuthor 
FROM 
	tblForumPosts 
WHERE 
	PostID = @ReplyPostID

SELECT 
	@StickyDate = StickyDate 
FROM 
	tblForumThreads 
WHERE 
	ThreadID = @ThreadID

IF @StickyDate < @ThreadDate
	SET @StickyDate = @ThreadDate

-- do the mass updates.
UPDATE 
	tblForumThreads
SET
	TotalReplies = (SELECT Count(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
	ThreadDate = @ThreadDate,
	StickyDate = @StickyDate,
	MostRecentPostAuthorID = @UserID,
	MostRecentPostAuthor = @PostAuthor,
	MostRecentPostID = @ReplyPostID
WHERE
	ThreadID = @ThreadID


-- find any lingering ParentIDs that don't match any posts in
-- our thread (from a merge or split action)
SET @FirstPostID = (	SELECT TOP 1 
				PostID 
			FROM 
				tblForumPosts
			WHERE
				ThreadID = @ThreadID
				AND IsApproved = 1
			ORDER BY
				PostDate ASC )

UPDATE
	tblForumPosts
SET
	ParentID = @FirstPostID
WHERE
	ParentID NOT IN (SELECT PostID FROM tblForumPosts WHERE ThreadID = @ThreadID)
	AND ThreadID = @ThreadID


-- fix the PostLevel and SortOrder ordering, by date
-- this could be done better, as it's on a MassScale now.
UPDATE
	tblForumPosts
SET
	PostLevel = 1,
	SortOrder = 1
WHERE
	ThreadID = @ThreadID
	AND PostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @ThreadID ORDER BY PostID ASC)

UPDATE
	tblForumPosts
SET
	PostLevel = 2,
	SortOrder = SortOrder + 1
WHERE
	ThreadID = @ThreadID
	AND PostID > @ReplyPostID

-- update the EmoticonID, if it's the first post
IF @ReplyPostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @ThreadID ORDER BY PostDate ASC)
	UPDATE
		tblForumThreads
	SET
		ThreadEmoticonID = (SELECT EmoticonID FROM tblForumPosts WHERE PostID = @ReplyPostID)
	WHERE
		ThreadID = @ThreadID	


SET NOCOUNT OFF
END



