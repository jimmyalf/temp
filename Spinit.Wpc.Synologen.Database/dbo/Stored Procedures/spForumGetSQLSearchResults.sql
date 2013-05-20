CREATE     PROCEDURE spForumGetSQLSearchResults
(
	@ForumID int,
	@PageSize int,
	@PageIndex int, 
	@SearchTerms nvarchar(500),
	@UserName nvarchar (50) = null
)
AS
	SET NOCOUNT ON

	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int
	DECLARE @RowsToReturn int
	DECLARE @MoreRecords int
	
	-- First set the rowcount
	SET @RowsToReturn = @PageSize * (@PageIndex + 1)
	SET @MoreRecords = @RowsToReturn + 1
	SET ROWCOUNT @MoreRecords

	-- Set the page bounds
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	-- Create a temp table to store the select results
	CREATE TABLE #PageIndex 
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		PostID int
	)

        -- Are we selecting from a specific forum?
        IF @ForumID > 0 AND @UserName is null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID = @ForumID AND ForumID NOT IN (SELECT ForumID from PrivateForums) ORDER BY PostDate Desc
        ELSE IF @ForumID = 0 AND @UserName is null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID NOT IN (SELECT ForumID from PrivateForums) ORDER BY PostDate Desc
	ELSE IF @ForumID > 0 AND @UserName is not null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID = @ForumID  AND (P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName))) ORDER BY PostDate Desc
	Else
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND (P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName))) ORDER BY PostDate Desc

	-- Do we have more records?
        IF (@MoreRecords > (SELECT count(*) FROM #PageIndex))
		SET @MoreRecords = @RowsToReturn

	-- Select the data out of the temporary table
	SELECT
		PageIndex.PostID,
		MoreRecords = @MoreRecords,
		P.ParentID,
		P.ThreadID,
		P.PostLevel,
		P.SortOrder,
		P.UserName,
		P.Subject,
		P.PostDate,
		P.ThreadDate,
		P.Approved,
		P.ForumID,
		F.Name As ForumName,
		Replies = 0,
		P.Body,
		P.TotalViews,
		P.IsLocked,
		HasRead = 0 -- not used
	FROM 
		#PageIndex PageIndex,
		Posts P,
		Forums F
	WHERE 
		P.PostID = PageIndex.PostID AND
		P.ForumID = F.ForumID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

	SET NOCOUNT OFF


