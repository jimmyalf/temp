CREATE          procedure spForumThreads_ThreadSet_Active
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@UserID int,
	@TotalReplies int = 1,
	@TotalViews int = 30,
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
	IF @ForumID > 0
		SET @TotalThreads = (select count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews)
	ELSE
		SET @TotalThreads = (select count(T.ThreadID) FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************
-- Unanswered Posts
IF @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews ORDER BY ThreadDate DESC
ELSE
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews ORDER BY ThreadDate DESC


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
	P.PostID = T.ThreadID AND
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
	

