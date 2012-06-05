CREATE PROCEDURE spForumUser_Password_Change
(
	@UserID int,
	@PasswordFormat int = 1,
	@NewPassword nvarchar(64),
	@Salt nvarchar(24)
)
AS
BEGIN
	UPDATE
		tblForumUsers
	SET
		[Password] = @NewPassword,
		PasswordFormat = @PasswordFormat,
		Salt = @Salt
	WHERE
		UserID = @UserID
END

