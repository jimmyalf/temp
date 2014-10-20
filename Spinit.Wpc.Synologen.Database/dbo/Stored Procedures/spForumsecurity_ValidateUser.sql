CREATE    procedure spForumsecurity_ValidateUser
(
	@UserName nvarchar(128),
	@Password nvarchar(128)
)
AS
	-- Update the time the user last logged in
	UPDATE 
		tblForumUsers
	SET 
		LastLogin = getdate()
	WHERE 
		UserName = @UserName
		AND Password = @Password
	
	SELECT @@ROWCOUNT 



