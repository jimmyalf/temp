CREATE    procedure spForumsystem_UserIsOnline (
	@UserID int,
	@LastAction nvarchar(1024)
)
AS
BEGIN
	-- First update the Users table
	UPDATE 
		tblForumUsers 
	SET 
		LastActivity = GetDate(),
		LastAction = @LastAction
	WHERE 
		UserID = @UserID

	-- Now update the lookup table
	IF EXISTS(SELECT UserID FROM tblForumUsersOnline WHERE UserID = @UserID)
		UPDATE tblForumUsersOnline SET LastActivity = GetDate(), LastAction = @LastAction WHERE UserID = @UserID
	ELSE
		INSERT INTO tblForumUsersOnline VALUES (@UserID, GetDate(), @LastAction)


END


