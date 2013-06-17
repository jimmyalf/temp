CREATE procedure spForumsystem_ACE_Get
(
	@ForumID int,
	@UserID int,
	@ACE binary(4) out
)
AS
BEGIN
	SET @ACE = 0x00000000
	SELECT @ACE = convert(int, @ACE) ^ ACE FROM tblForumForumPermissions P, tblForumUserRoles R WHERE P.RoleID = R.RoleID AND R.UserID = @UserID AND P.ForumID = @ForumID
END


