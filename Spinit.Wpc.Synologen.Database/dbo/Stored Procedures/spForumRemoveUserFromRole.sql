CREATE procedure spForumRemoveUserFromRole
(
   @UserID	int,
   @RoleID	int
)
AS
IF EXISTS (SELECT UserID FROM tblForumUsersInRoles WHERE UserID = @UserID AND @RoleID = @RoleID)
DELETE FROM
    tblForumUsersInRoles
WHERE
    	UserID	= @UserID
	AND RoleID	= @RoleID


