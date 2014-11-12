CREATE     PROCEDURE spForumGetSearchResults
(
	@SearchTerms	nvarchar(500),
	@Page int,
	@RecsPerPage int,
	@UserName nvarchar(50)
)
 AS
	CREATE TABLE #tmp
	(
		ID int IDENTITY,
		PostID int
	)
	DECLARE @sql nvarchar(1000)
	SET NOCOUNT ON
	SELECT @sql = 'INSERT INTO #tmp(PostID) SELECT PostID ' + 
			'FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID ' +
			@SearchTerms + ' ORDER BY ThreadDate DESC'
	EXEC(@sql)

	-- ok, all of the rows are inserted into the table.
	-- now, select the correct subset
	DECLARE @FirstRec int, @LastRec int
	SELECT @FirstRec = (@Page - 1) * @RecsPerPage
	SELECT @LastRec = (@Page * @RecsPerPage + 1)
	DECLARE @MoreRecords int
	SELECT @MoreRecords = COUNT(*)  FROM #tmp -- WHERE ID >= @LastRec


	-- Select the data out of the temporary table
	IF @UserName IS NOT NULL
		SELECT
			T.PostID,
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
			MoreRecords = @MoreRecords,
			Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
			P.Body,
			P.TotalViews,
			P.IsLocked,
			HasRead = 0 -- not used
		FROM 
			#tmp T
			INNER JOIN Posts P (nolock) ON
				P.PostID = T.PostID
			INNER JOIN Forums F (nolock) ON
				F.ForumID = P.ForumID
		WHERE 
			T.ID > @FirstRec AND ID < @LastRec AND
			(P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR
			P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName)))
	ELSE
		SELECT
			T.PostID,
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
			MoreRecords = @MoreRecords,
			Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
			P.Body,
			P.TotalViews,
			P.IsLocked,
			HasRead = 0 -- not used
		FROM 
			#tmp T
			INNER JOIN Posts P (nolock) ON
				P.PostID = T.PostID
			INNER JOIN Forums F (nolock) ON
				F.ForumID = P.ForumID
		WHERE 
			T.ID > @FirstRec AND ID < @LastRec AND
			P.ForumID NOT IN (SELECT ForumID from PrivateForums)

	SET NOCOUNT OFF


