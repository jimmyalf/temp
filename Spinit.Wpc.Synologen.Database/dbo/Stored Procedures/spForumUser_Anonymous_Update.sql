CREATE     PROCEDURE spForumUser_Anonymous_Update
(
	@UserID char(36),
	@LastActivity datetime,
	@LastAction nvarchar(1024) = ''
)
AS
BEGIN
	-- Does the user already exist?
	IF EXISTS (SELECT UserID FROM tblForumAnonymousUsers WHERE UserID = @UserID)

		UPDATE 
			tblForumAnonymousUsers
		SET 
			LastLogin = @LastActivity,
			LastAction = @LastAction
		WHERE
			UserID = @UserID

	ELSE

		INSERT INTO
			tblForumAnonymousUsers
			(UserID, LastLogin, LastAction) 
		VALUES
			(@UserID, @LastActivity, @LastAction)

END
 
