CREATE    PROCEDURE spForumGetSearchResultsByUser (
    @Page int,
    @RecsPerPage int,
    @ForumID int = 0,
    @UserPattern nvarchar(50),
    @UserName nvarchar(50) = NULL,
    @MoreRecords bit output,
    @Status bit output
) AS


    -- Performance optimizations
    SET NOCOUNT ON
    -- Global declarations
    DECLARE @sql nvarchar(1000)
    DECLARE @FirstRec int, @LastRec int, @MoreRec int

    SET @FirstRec = (@Page - 1) * @RecsPerPage;
    SET @LastRec = (@FirstRec + @RecsPerPage);
    SET @MoreRec = @LastRec + 1;
    SET @MoreRecords = 0;

    CREATE TABLE #SearchResults (
        IndexID int IDENTITY(1,1),
        PostID int
    )

    -- Turn on rowcounting for performance
    SET ROWCOUNT @MoreRec;
    INSERT INTO #SearchResults(PostID)
    SELECT PostID
    FROM Posts P (nolock)
    WHERE
        Approved = 1 AND
        (
            @ForumID = 0 OR
            ForumID = @ForumID
        ) AND
        (
            P.ForumID NOT IN (SELECT ForumID FROM PrivateForums) OR
            P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName FROM UsersInRoles WHERE UserName = @UserName))
        ) AND
        0 < ISNULL(PATINDEX(@UserPattern, Username), 1)
    ORDER BY ThreadDate DESC
    IF @@ROWCOUNT > @LastRec SET @MoreRecords = 1
    SET ROWCOUNT 0
    -- Turn off rowcounting

    -- Select the data out of the temporary table
    SELECT
        P.*,
	ForumName = F.Name,
	Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
        HasRead = 0 -- not used
    FROM 
        Posts P (nolock), Forums F, #SearchResults
    WHERE
        P.PostID = #SearchResults.PostID AND
        P.ForumID = F.ForumID AND
        #SearchResults.IndexID > @FirstRec AND
        #SearchResults.IndexID <= @LastRec
    ORDER BY #SearchResults.IndexID ASC

