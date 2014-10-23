CREATE       PROCEDURE spForumModerate_PostSet
(
	@ForumID		int,
	@PageIndex 		int,
	@PageSize 		int,
	@SortBy 		int,
	@SortOrder 		bit,
	@UserID 		int,
	@ReturnRecordCount 	bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @ThreadID int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	PostID int
)

-- Sort by Post Date
IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY PostDate

ELSE IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY PostDate DESC

-- Sort by Author
IF @SortBy = 1 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY Username

ELSE IF @SortBy = 1 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY Username DESC

-- Select the individual posts
SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	Username,
	Replies = (SELECT COUNT(*) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	IsModerator = (SELECT count(*) from tblForumModerators where UserID = @UserID),
	HasRead = 0 -- not used
FROM 
	tblForumPosts P (nolock),
	tblForumThreads T,
	tblForumUsers U,
        tblForumUserProfile UP,
	#PageIndex
WHERE 
	P.PostID = #PageIndex.PostID AND
	P.UserID = U.UserID AND
	T.ThreadID = P.ThreadID AND
        U.UserID = UP.UserID AND
	#PageIndex.IndexID > @PageLowerBound AND
	#PageIndex.IndexID < @PageUpperBound
END



