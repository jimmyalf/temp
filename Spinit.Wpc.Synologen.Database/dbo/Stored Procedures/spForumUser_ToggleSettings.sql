CREATE   procedure spForumUser_ToggleSettings
(
	@UserID int,
	@ModerationLevel int,
	@UserAccountStatus int,
	@ForceLogin bit = 0,
	@IsAvatarApproved bit,
	@ModeratorID int
)
AS
BEGIN

	UPDATE 
		tblForumUsers 
	SET
		UserAccountStatus = @UserAccountStatus,
		ForceLogin = @ForceLogin
	WHERE
		UserID = @UserID

	UPDATE
		tblForumUserProfile
	SET
		ModerationLevel = @ModerationLevel,
		IsAvatarApproved = @IsAvatarApproved
	WHERE
		UserID = @UserID

	IF @ModerationLevel = 0
	  exec spForumsystem_ModerationAction_AuditEntry 11, @ModeratorID, null, @UserID
	ELSE	
	  exec spForumsystem_ModerationAction_AuditEntry 10, @ModeratorID, null, @UserID

	IF @UserAccountStatus = 1
	  exec spForumsystem_ModerationAction_AuditEntry 13, @ModeratorID, null, @UserID
	ELSE	
	  exec spForumsystem_ModerationAction_AuditEntry 12, @ModeratorID, null, @UserID

END


