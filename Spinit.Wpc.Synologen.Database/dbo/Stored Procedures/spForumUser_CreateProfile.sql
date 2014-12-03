create           procedure spForumUser_CreateProfile 
(
	@UserID int,
	@StringNameValuePairs  varbinary (7500) = 0,
	@TimeZone   float = 0.0,
	@PostRank   binary(1) = 0x0,
	@PostSortOrder   int = 0,
	@IsAvatarApproved   smallint  = 0,
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
BEGIN
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
	
	-- exit the sproc normally
	RETURN
END
