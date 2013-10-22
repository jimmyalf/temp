CREATE             PROCEDURE spForumTrackAnonymousUsers
(
	@UserID char(36)
)
AS
BEGIN
	-- Does the user already exist?
	IF EXISTS (SELECT UserID FROM AnonymousUsers WHERE UserID = @UserID)
		UPDATE 
			AnonymousUsers
		SET 
			LastLogin = GetDate()
		WHERE
			UserID = @UserID
	ELSE
		INSERT INTO
			AnonymousUsers
			(UserID) 
		VALUES
			(@UserID)
	
	-- Anonymous users also pay tax to clean up table
	DELETE AnonymousUsers WHERE LastLogin < DateAdd(minute, -20, GetDate())	
END

