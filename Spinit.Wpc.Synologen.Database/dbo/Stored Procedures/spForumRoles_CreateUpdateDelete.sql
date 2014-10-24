CREATE PROCEDURE spForumRoles_CreateUpdateDelete
(
	@RoleID	int out,
	@DeleteRole	bit = 0,
	@Name		nvarchar(256) = '',
	@Description	nvarchar(512) = ''
)
AS

-- Are we deleting the role?
IF @DeleteRole = 1
BEGIN

	-- delete all users in the role
	DELETE 
		tblForumUsersInRoles
	WHERE 
		RoleID = @RoleID

	-- delete all forums using the role
	DELETE 
		tblForumForumPermissions
	WHERE 
		RoleID = @RoleID


	-- finally we can delete the actual role
	DELETE 
		tblForumRoles
	WHERE 
		RoleID = @RoleID

	RETURN
END

-- Are we updating a forum
IF  @RoleID > 0
BEGIN
	-- Update the role
	UPDATE 
		tblForumRoles
	SET
		Name = @Name,
		Description = @Description
	WHERE 
		RoleID = @RoleID
END
ELSE
BEGIN

	-- Create a new Forum
	INSERT INTO 
		tblForumRoles (
			Name, 
			Description
			)
		VALUES (
			@Name,
			@Description
			)
	
	SET @RoleID = @@Identity

END


