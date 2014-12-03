CREATE       procedure spForumThreads_ThreadSet_Unanswered
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@ThreadsNewerThan Datetime,
	@UserID int,
	@UnreadOnly bit,
	@ReturnRecordCount bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalThreads int

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

IF (@ReturnRecordCount = 1)
  IF @UnreadOnly = 1 AND @ForumID > 0
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE IF @UnreadOnly = 1
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE IF @UnreadOnly = 0 AND @ForumID > 0
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ThreadDate >= @ThreadsNewerThan AND ForumID > 0 AND TotalReplies = 0)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

IF @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 ORDER BY ThreadDate DESC
ELSE
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND F.ForumID > 0 AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 ORDER BY ThreadDate DESC, F.ForumID DESC

IF @UnreadOnly = 1
	-- Get Unread Topics only
		SELECT 
		T.*,
		P.Subject,
		P.Body,
		U.UserName,
		HasRead = 1
	FROM 
		tblForumThreads T (nolock),
		tblForumUsers U,
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ForumID = @ForumID AND 
		T.UserID = U.UserID AND
		P.PostID = T.ThreadID AND
		ThreadDate >= @ThreadsNewerThan AND
		T.ThreadID NOT IN (SELECT PostsRead.PostID FROM PostsRead WHERE PostsRead.UserID = @UserID) AND
		T.ThreadID >= (select MarkReadAfter from ForumsRead where UserID = @UserID and forumid = @ForumID) AND
		T.ThreadID = PageIndex.ThreadID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID
ELSE
	SELECT 
		T.*,
		P.Subject,
		P.Body,
		U.UserName,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumUsers U,
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
		T.UserID = U.UserID AND
		T.ThreadID = P.PostID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

-- Update that the user has read this forum
IF @UserID > 0
	exec spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	SELECT @TotalThreads


END
	

