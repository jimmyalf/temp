CREATE                  procedure spForumThreads_ThreadSet
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@ThreadsNewerThan Datetime,
	@SortBy int,
	@SortOrder bit,
	@UserID int,
	@UnreadOnly bit,
	@Unanswered bit,
	@ReturnRecordCount bit
)
AS
BEGIN

-- Are we attempting to get unanswered messages?
IF @Unanswered = 1
BEGIN

	exec spForumThreads_ThreadSet_Unanswered @ForumID, @PageIndex, @PageSize, @ThreadsNewerThan, @UserID, @UnreadOnly, @ReturnRecordCount

	RETURN
END

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
  IF (@UnreadOnly = 1)
    SELECT @TotalThreads = count(T.ThreadID) FROM tblForumThreads T WHERE T.ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan AND IsApproved = 1 AND dbo.sfForumHasReadPost(@UserID, T.ThreadID, T.ForumID) = 0
  ELSE
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan AND IsApproved = 1)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

-- Sort by Last Post
IF @SortBy = 0 AND @SortOrder = 1 AND @UnreadOnly = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 ORDER BY IsSticky DESC, StickyDate DESC
ELSE IF @SortBy = 0 AND @SortOrder = 1 AND @UnreadOnly = 1
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 AND dbo.sfForumHasReadPost(@UserID, T.ThreadID, T.ForumID) = 0 ORDER BY IsSticky DESC, StickyDate DESC
ELSE IF @SortBy = 0 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 ORDER BY IsSticky, StickyDate

-- Sort by Thread Author
IF @SortBy = 1 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumUsers U WHERE T.ForumID = @ForumID AND T.ThreadDate >= @ThreadsNewerThan AND T.UserID = U.UserID  AND IsApproved = 1 ORDER BY Username
ELSE IF @SortBy = 1 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumUsers U WHERE T.ForumID = @ForumID AND T.ThreadDate >= @ThreadsNewerThan AND T.UserID = U.UserID AND IsApproved = 1 ORDER BY Username DESC

-- Sort by Replies
IF @SortBy = 2 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalReplies
ELSE IF @SortBy = 2 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalReplies DESC

-- Sort by Views
IF @SortBy = 3 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalViews
ELSE IF @SortBy = 3 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalViews DESC

-- Unanswered Posts
IF @Unanswered = 1 AND @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 AND IsApproved = 1 ORDER BY ThreadDate DESC
ELSE IF @Unanswered = 1
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 AND IsApproved = 1 ORDER BY ThreadDate DESC, F.ForumID DESC

IF @UnreadOnly = 1
	SELECT 
		T.*,
		P.Subject,
		P.Body,
		UserName = T.PostAuthor,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
		P.PostID = T.ThreadID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

ELSE
		SELECT 
		T.*,
		P.Subject,
		P.Body,
		UserName = T.PostAuthor,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
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


