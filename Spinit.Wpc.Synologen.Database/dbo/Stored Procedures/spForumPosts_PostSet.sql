CREATE         PROCEDURE spForumPosts_PostSet
(
	@PostID	int,
	@PageIndex int,
	@PageSize int,
	@SortBy int,
	@SortOrder bit,
	@UserID int,
	@ReturnRecordCount bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @ThreadID int
DECLARE @ForumID int

-- First set the rowcount
DECLARE @RowsToReturn int
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Get the ThreadID
SELECT
	@ThreadID = ThreadID,
	@ForumID = ForumID
FROM 
	tblForumPosts 
WHERE 
	PostID = @PostID

-- Is the Forum 0 (If so this is a private message and we need to verify the user can view it
IF @ForumID = 0
BEGIN
	IF NOT EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID AND ThreadID = @ThreadID)
		RETURN
END

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	PostID int
)

-- Sort by Post Date
IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY PostDate

ELSE IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY PostDate DESC

-- Sort by Author
IF @SortBy = 1 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY UserID

ELSE IF @SortBy = 1 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY UserID DESC

-- Sort by SortOrder
IF @SortBy = 2 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY SortOrder

ELSE IF @SortBy = 2 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY SortOrder DESC


-- Select the individual posts
SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	--Username = P.PostAuthor,
	Username = dbo.sfForumGetUserDisplayName(P.UserID),
	EditNotes = (SELECT EditNotes FROM tblForumPostEditNotes WHERE PostID = P.PostID),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	IsModerator = (SELECT count(UserID) from tblForumModerators where UserID = @UserID),
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
ORDER BY
	IndexID
END

IF @ReturnRecordCount = 1
  SELECT count(PostID) FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID
