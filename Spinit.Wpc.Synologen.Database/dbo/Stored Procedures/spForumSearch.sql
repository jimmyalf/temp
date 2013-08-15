CREATE    procedure spForumSearch (
	@SearchSQL nvarchar(4000),
	@RecordCountSQL nvarchar(4000),
	@PageIndex int = 0,
	@PageSize int = 25
)
AS
BEGIN

	DECLARE @StartTime datetime
	DECLARE @RowsToReturn int
	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int
	DECLARE @Count int

	-- Used to calculate cost of query
	SET @StartTime = GetDate()

	-- Set the rowcount
	SET @RowsToReturn = @PageSize * (@PageIndex + 1)
	SET ROWCOUNT @RowsToReturn

	-- Calculate the page bounds
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	-- Create a temp table to store the results in
	CREATE TABLE #SearchResults
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		PostID int,
		ForumID int,
		Weight int,
		PostDate datetime
	)

	-- Fill the temp table
	INSERT INTO #SearchResults (PostID, ForumID, Weight, PostDate)
	exec (@SearchSQL)

	-- SELECT actual search results from this table
	SELECT
		UserName = dbo.sfForumGetUserDisplayName(U.UserID),
		P.*,
		U.*,
		T.ThreadDate,
		T.IsLocked,
		T.IsSticky,
		UP.*,
		AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
		Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
		IsModerator = (SELECT count(*) from tblForumModerators where UserID = P.UserID),
		HasRead = 0 -- not used
	FROM 
		tblForumPosts P,
		tblForumUsers U,
		tblForumThreads T,
		tblForumUserProfile UP,
		#SearchResults R
	WHERE
		P.PostID = R.PostID AND
		T.ThreadID = P.ThreadID AND
		U.UserID = P.UserID AND
		P.UserID = UP.UserID AND
		R.IndexID > @PageLowerBound AND
		R.IndexID < @PageUpperBound

	-- Do we need to return a record estimate?
	exec (@RecordCountSQL)

	SELECT Duration = GetDate() - @StartTime
END
