create procedure spForumRole_AddUser
(
   @UserID int,
   @RoleID int
)
AS
IF NOT EXISTS (SELECT UserID FROM tblForumUsersInRoles WHERE UserID = @UserID AND RoleID = @RoleID)
INSERT INTO
	tblForumUsersInRoles
VALUES
	(@UserID, @RoleID)


