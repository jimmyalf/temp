CREATE   procedure spForumRoles_Get
(
@UserID int = 0
)
AS
BEGIN

	IF (@UserID = 0)
		SELECT
			*
		FROM
			tblForumRoles
	ELSE
		SELECT DISTINCT
			* 
		FROM 
			tblForumUsersInRoles U,
			tblForumRoles R
		WHERE
			U.RoleID = R.RoleID AND
			UserID = @UserID
END



