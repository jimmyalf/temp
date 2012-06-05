CREATE      PROCEDURE spForumUser_Get
(
	@UserID int,
	@UserName nvarchar(64) = '',
	@IsOnline bit = 0,
	@LastAction nvarchar(1024) = ''
)
AS
BEGIN
	
	-- Are we looking up the user by username or ID?
	IF @UserID = 0
	BEGIN
		SELECT
			U.UserID,
			U.Salt,
			--U.UserName,
			UserName = dbo.sfForumGetUserDisplayName(U.UserID),
			U.PasswordFormat,
			U.PasswordQuestion,
			U.Email,
			U.DateCreated,
			U.LastLogin,
			U.LastActivity,
			U.LastAction,
			U.UserAccountStatus,
			U.IsAnonymous,
			U.ForceLogin,
			P.*
		FROM
			tblForumUsers U LEFT OUTER JOIN tblForumUserProfile P
			ON U.UserID = P.UserID
		WHERE 	UserName = @UserName
		
		-- Get the Username
		SET @UserID = (SELECT UserID FROM tblForumUsers U WHERE U.UserName = @UserName)
		
	END
	ELSE BEGIN
		-- Looking up the user by ID
	
		-- Get the user details
		SELECT
			U.UserID,
			U.Salt,
			--U.UserName,
			UserName = dbo.sfForumGetUserDisplayName(U.UserID),
			U.PasswordFormat,
			U.PasswordQuestion,
			U.Email,
			U.DateCreated,
			U.LastLogin,
			U.LastActivity,
			U.LastAction,
			U.UserAccountStatus,
			U.IsAnonymous,
			U.ForceLogin,
			P.*
		FROM 
			tblForumUsers U LEFT OUTER JOIN tblForumUserProfile P
			ON U.UserID = P.UserID
		WHERE 	U.UserID = @UserID

	END
	
	IF @IsOnline = 1
	BEGIN
		EXEC spForumsystem_UserIsOnline @UserID, @LastAction
	END

END
