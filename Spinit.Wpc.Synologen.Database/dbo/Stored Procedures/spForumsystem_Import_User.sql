CREATE PROCEDURE spForumsystem_Import_User
	@UserID [int],
	@Password [nvarchar](64) = '',
	@Email [nvarchar](128) = '',
	@DateCreated [datetime],
	@UserName [nvarchar](64) = '',
	@AccountStatus [smallint] = 1,
	@TimeZone [int] = 0,
	@EnableAvatar [smallint] = 0,
	@EnableDisplayInMemberList [smallint] = 1,
	@StringNameValuePairs [varbinary](7500) = 0,
	@EnableThreadTracking [smallint] = 0


AS
BEGIN

	-- Are we creating a user? 
	IF EXISTS(SELECT * FROM tblForumUsers WHERE UserID = @UserID)
		RETURN
	ELSE
	BEGIN

			SET IDENTITY_INSERT tblForumUsers ON
			INSERT INTO 
				tblForumUsers 
				(
					UserID,
					UserName, 
					Email, 
					Password, 
					UserAccountStatus,
					DateCreated
				)
			VALUES	
				(
					@UserID,
					@UserName, 
					@Email, 
					@Password, 
					@AccountStatus,
					@DateCreated
				)
			SET IDENTITY_INSERT tblForumUsers OFF

			INSERT INTO
				tblForumUserProfile
				(
					UserID,
					TimeZone,
					EnableAvatar,
					EnableDisplayInMemberList,
					StringNameValues,	
					EnableThreadTracking
				)
			VALUES
				(
					@UserID,
					@TimeZone,
					@EnableAvatar,
					@EnableDisplayInMemberList,
					@StringNameValuePairs,
					@EnableThreadTracking
				)
	END

END


