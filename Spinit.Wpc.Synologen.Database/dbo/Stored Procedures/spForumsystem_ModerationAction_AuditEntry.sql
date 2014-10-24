create procedure spForumsystem_ModerationAction_AuditEntry
(
	@ModerationAction int,
	@ModeratorID int,
	@PostID int = null,
	@UserID int = null,
	@ForumID int = null,
	@Notes nvarchar(1024) = null
)
AS
BEGIN
	INSERT INTO
		tblForumModerationAudit
		(
			ModerationAction,
			PostID,
			UserID,
			ForumID,
			ModeratorID,
			Notes
		)
	VALUES
		(
			@ModerationAction,
			@PostID,
			@UserID,
			@ForumID,
			@ModeratorID,
			@Notes
		)

	UPDATE
		tblForumModerationAction
	SET
		TotalActions = TotalActions + 1
	WHERE
		ModerationAction = @ModerationAction

		
END

