CREATE PROCEDURE spForumStatistics_GetMostActiveUsers_Filtered (
    @RoleFilter nvarchar(50) = NULL,
    @StartDate datetime = NULL,
    @EndDate datetime = NULL
) AS
    IF @StartDate IS NULL
        IF @EndDate IS NULL SET @StartDate = DATEADD(dd, -1, GETDATE())
        ELSE SET @StartDate = DATEADD(dd, -1, @EndDate)
        
    IF @EndDate IS NULL SET @EndDate = GETDATE()
    IF @StartDate > @EndDate BEGIN
        DECLARE @TempDate datetime;
        SET @TempDate = @StartDate
        SET @StartDate = @EndDate
        SET @EndDate = @TempDate
    END
    
    SELECT TOP 10
        Username, 
        TotalPosts = COUNT(*)
    FROM 
        Posts P 
    WHERE 
        PostDate >= @StartDate AND
        PostDate <= @EndDate AND
        (
            @RoleFilter IS NULL OR
            EXISTS(SELECT NULL FROM UsersInRoles WHERE UserName=P.Username AND RoleName=@RoleFilter)
        )
    GROUP BY P.Username
    ORDER BY TotalPosts DESC



