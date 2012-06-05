CREATE PROCEDURE spForumGetSearchResultsByText_FTQ (
    @Pattern1 nvarchar(250),
    @ForumID int,
    @Username nvarchar(50)
) AS
    IF @@NESTLEVEL > 1 BEGIN
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
                P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName FROM UsersInRoles WHERE Username = @Username))
            ) AND
            CONTAINS(Body, @Pattern1)
        ORDER BY ThreadDate DESC
    END


