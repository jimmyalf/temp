CREATE PROCEDURE spForumDisallowedName_CreateUpdateDelete
(
	@Name		nvarchar(64),
	@Replacement 	nvarchar(64),
	@DeleteName	bit = 0
)
AS

SET NOCOUNT ON

if( @DeleteName > 0 )
BEGIN
	DELETE FROM
		tblForumDisallowedNames
	WHERE
		DisallowedName = @Name
END
ELSE 
BEGIN
		UPDATE tblForumDisallowedNames SET
			DisallowedName = @Replacement
		WHERE
			DisallowedName = @Name

	if( @@rowcount = 0 )
	BEGIN
		INSERT INTO tblForumDisallowedNames (
			DisallowedName
		) VALUES (
			@Name
		)
		
	END
END


