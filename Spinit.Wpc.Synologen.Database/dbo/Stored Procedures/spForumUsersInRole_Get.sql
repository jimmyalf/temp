CREATE PROCEDURE spForumUsersInRole_Get
(
	@PageIndex int,
	@PageSize int,
	@SortBy int = 0,
	@SortOrder int = 0,
	@RoleID int,
	@UserAccountStatus smallint = 1,
	@ReturnRecordCount bit = 0
)
AS
BEGIN
DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalUsers int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Create a temp table to store the select results
CREATE TABLE #PageIndexForUsers 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	UserID int
)	

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
    SET @TotalUsers = (SELECT count(R.UserID) FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

-- Sort by Date Joined
IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY DateCreated
ELSE IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY DateCreated DESC

-- Sort by username
IF @SortBy = 1 AND @SortOrder = 1
	INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY UserName
ELSE IF @SortBy = 1 AND @SortOrder = 0
	INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY UserName DESC

-- Sort by Last Active
IF @SortBy = 3 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY LastActivity DESC
ELSE IF @SortBy = 3 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY LastActivity

-- Sort by TotalPosts
IF @SortBy = 4 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY TotalPosts DESC
ELSE IF @SortBy = 4 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY TotalPosts

-- Get the user details
SELECT
	*,
	IsModerator = (SELECT Count(*) FROM tblForumModerators WHERE UserID = U.UserID)
FROM 
	tblForumUsers U (nolock),
	tblForumUserProfile P,
	#PageIndexForUsers
WHERE 
	U.UserID = #PageIndexForUsers.UserID AND
	U.UserID = P.UserID AND
	#PageIndexForUsers.IndexID > @PageLowerBound AND
	#PageIndexForUsers.IndexID < @PageUpperBound
ORDER BY
	#PageIndexForUsers.IndexID
END

-- Return the record count if necessary

IF (@ReturnRecordCount = 1)
  SELECT @TotalUsers


