create procedure spForumRole_Get
(
	@RoleID int
)
AS
BEGIN
SELECT * FROM tblForumRoles WHERE RoleID = @RoleID
END


