CREATE           procedure spForumUser_CreateUpdateDelete 
(
	@UserID int out,
	@UserName   nvarchar (64) = '',
	@Password   nvarchar (64) = '',
	@Email    nvarchar (128) = '',
	@StringNameValuePairs  varbinary (7500) = 0,
	@UserAccountStatus  smallint = 1,
	@IsAnonymous   smallint = 0,
	@PasswordFormat  int = 1, 
	@PasswordQuestion  nvarchar(256) = '',
	@PasswordAnswer  nvarchar(256) = '',
	@Salt    nvarchar (24) = '',
	@AppUserToken       varchar (128) = '',
	@ForumView   int = 0,
	@TimeZone   float = 0.0,
	@PostRank   binary(1) = 0x0,
	@PostSortOrder   int = 0,
	@IsAvatarApproved   smallint  = 0,
	@ForceLogin   bit   = 0,
	@ModerationLevel  smallint  = 0,
	@EnableThreadTracking  smallint  = 0,
	@EnableDisplayUnreadThreadsOnly smallint  = 0,
	@EnableAvatar    smallint  = 0,
	@EnableDisplayInMemberList  smallint  = 1,
	@EnablePrivateMessages  smallint  = 1,
	@EnableOnlineStatus   smallint  = 1,
	@EnableHtmlEmail   smallint  = 1,
	@Action    int
)
AS

-- this sproc returns various error/success codes
-- a return value of 1 means success
-- a return value of 2 means a dup username
-- a return value of 3 means a dup email address
-- first, we need to check if the username is a dup


-- Are we creating a user?
IF @Action = 0
BEGIN
	IF @IsAnonymous = 1
	BEGIN
		SELECT @UserID = UserID FROM tblForumUsers WHERE UserName = @UserName AND IsAnonymous = 1
		
		-- Check if the anonymous user already exists
		IF @UserID IS NOT NULL
		BEGIN
			SELECT 1
		RETURN
	END

END
	
-- check for username exists
IF EXISTS(SELECT UserName FROM tblForumUsers (nolock) WHERE UserName = @UserName AND IsAnonymous = 0)
	SELECT 2
ELSE
	-- we need to check if the email is a dup
	IF EXISTS(SELECT Email FROM tblForumUsers (nolock) WHERE Email = @Email AND IsAnonymous = 0)
		SELECT 3
	ELSE
	BEGIN
		-- INSERT the user
		INSERT INTO tblForumUsers 
			( UserName, 
			Email, 
			Password, 
			PasswordFormat,
			Salt,
			PasswordQuestion,
			PasswordAnswer,
			UserAccountStatus,
			IsAnonymous,
			AppUserToken )
		VALUES 
			( @UserName, 
			@Email, 
			@Password, 
			@PasswordFormat,
			@Salt,
			@PasswordQuestion,
			@PasswordAnswer, 
			@UserAccountStatus,
			@IsAnonymous,
			@AppUserToken )
		
		-- Get the new userID
		SET @UserID = @@IDENTITY
		
		INSERT INTO tblForumUserProfile
			VALUES
			( @UserID,
			@TimeZone,
			0,
			@PostSortOrder,
			@StringNameValuePairs,
			@PostRank,
			@IsAvatarApproved,
			@ModerationLevel,
			@EnableThreadTracking,
			@EnableDisplayUnreadThreadsOnly,
			@EnableAvatar,
			@EnableDisplayInMemberList,
			@EnablePrivateMessages,
			@EnableOnlineStatus,
			@EnableHtmlEmail )
		
		SELECT 1 -- return Everything's fine status code
	END
	
	-- exit the sproc normally
	RETURN
END

-- Update the user
ELSE IF @Action = 1
BEGIN
	-- First Update the tblForumUsers table
	UPDATE
		tblForumUsers
	SET
		UserName = @UserName,
		Email = @Email,
		UserAccountStatus = @UserAccountStatus,
		ForceLogin = @ForceLogin
	WHERE
		UserID = @UserID
	
	-- Next, update the user's profile
	UPDATE
		tblForumUserProfile
	SET
		TimeZone = @TimeZone,
		PostRank = @PostRank,
		PostSortOrder = @PostSortOrder,
		StringNameValues = @StringNameValuePairs,
		IsAvatarApproved = @IsAvatarApproved,
		ModerationLevel = @ModerationLevel,
		EnableThreadTracking = @EnableThreadTracking,
		EnableDisplayUnreadThreadsOnly = @EnableDisplayUnreadThreadsOnly,
		EnableAvatar = @EnableAvatar,
		EnableDisplayInMemberList = @EnableDisplayInMemberList,
		EnablePrivateMessages = @EnablePrivateMessages,
		EnableOnlineStatus = @EnableOnlineStatus,
		EnableHtmlEmail = @EnableHtmlEmail
	WHERE
		UserID = @UserID

END

