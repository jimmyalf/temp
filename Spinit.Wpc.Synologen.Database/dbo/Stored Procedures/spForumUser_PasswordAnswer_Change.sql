CREATE PROCEDURE spForumUser_PasswordAnswer_Change
(
	@UserID int,
	@PasswordQuestion  nvarchar(256),
	@PasswordAnswer  nvarchar(256)
)
AS
BEGIN
	UPDATE
		tblForumUsers
	SET
		PasswordQuestion = @PasswordQuestion,
		PasswordAnswer = @PasswordAnswer
	WHERE
		UserID = @UserID
END


