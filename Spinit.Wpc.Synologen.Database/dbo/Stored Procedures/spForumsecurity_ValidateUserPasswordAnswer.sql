CREATE PROCEDURE spForumsecurity_ValidateUserPasswordAnswer
(
	@UserID int,
	@PasswordAnswer nvarchar(256)
)
AS
BEGIN
	SELECT 
		COUNT(UserID) 
	FROM 
		tblForumUsers
	WHERE
		UserID = @UserID
		AND (PasswordAnswer = @PasswordAnswer OR PasswordAnswer = NULL)
END


