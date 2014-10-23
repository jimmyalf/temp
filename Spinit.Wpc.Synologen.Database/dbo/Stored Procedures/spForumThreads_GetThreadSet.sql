CREATE   procedure spForumThreads_GetThreadSet (
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@sqlCount nvarchar(4000),
	@sqlPopulate nvarchar(4000),
	@UserID int,
	@ReturnRecordCount bit)
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

-- Create a temp table to store the select results
CREATE TABLE #PageIndex (
	IndexID int IDENTITY (1, 1) NOT NULL,
	HasRead bit,
	ThreadID int
)

INSERT INTO #PageIndex (ThreadID, HasRead)
EXEC (@sqlPopulate)

SELECT 
	--jT.*,
	jT.ThreadID,
	jT.ForumID,
	jT.UserID,
	jT.PostAuthor,
	jT.PostDate,
	jT.ThreadDate,
	jT.LastViewedDate,
	jT.StickyDate,
	jT.TotalViews,
	jT.TotalReplies,
	jT.MostRecentPostAuthorID,
	--jT.MostRecentPostAuthor,
	MostRecentPostAuthor = dbo.sfForumGetUserDisplayName(jT.MostRecentPostAuthorID),
	jT.MostRecentPostID,
	jT.IsLocked,
	jT.IsSticky,
	jT.IsApproved,
	jT.RatingSum,
	jT.TotalRatings,
	jT.ThreadEmoticonID,
	jT.ThreadStatus,
	HasRead = jPI.HasRead,
	jP.PostID,
	jP.Subject,
	jP.Body,
	--UserName = jT.PostAuthor
	UserName = dbo.sfForumGetUserDisplayName(jT.UserID)
FROM 
	#PageIndex jPI
	JOIN tblForumThreads jT ON jPI.ThreadID = jT.ThreadID
	JOIN tblForumPosts jP ON jPI.ThreadID = jP.ThreadID
WHERE 
	jPI.IndexID > @PageLowerBound
	AND jPI.IndexID < @PageUpperBound
	AND jP.PostLevel = 1 	-- PostLevel=1 should mean it's a top-level thread starter
ORDER BY
	jPI.IndexID	-- this is the ordering system we're using populated from the @sqlPopulate

DROP TABLE #PageIndex

-- Update that the user has read this forum
IF @UserID > 0
	EXEC spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	EXEC (@sqlCount)

-- Return the users that the message is to if this
-- is a private message
IF @ForumID = 0
	SELECT 
		U.*,
		UP.*,
		P2.ThreadID 
	FROM
		tblForumPrivateMessages P1, 
		tblForumPrivateMessages P2,
		tblForumUsers U,
		tblForumUserProfile UP
	WHERE 
		P1.UserID = @UserID AND 
		P2.UserID <> @UserID AND 
		P2.UserID = U.UserID AND
		U.UserID = UP.UserID AND
		P1.ThreadID = P2.ThreadID

END
