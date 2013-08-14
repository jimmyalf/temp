CREATE   procedure spForumPost_ToggleSettings
(
	@PostID int,
	@IsAnnouncement bit,
	@IsLocked bit,
	@ModeratorID int
)
AS
BEGIN
	DECLARE @CurrentLockState bit
	DECLARE @CurrentAnnouncementState bit
	
	DECLARE @ThreadID int
	DECLARE @PostLevel int
	SELECT 
		@ThreadID = ThreadID, 
		@PostLevel = PostLevel 
	FROM 
		tblForumPosts 
	WHERE 
		PostID = @PostID

-- Is this a thread
IF @PostLevel =1
BEGIN
	print 'Toggling settings on a thread.'

	-- Get the current state of the thread
	SELECT 
		@CurrentLockState = IsLocked
	FROM 
		tblForumThreads 
	WHERE 
		ThreadID = @ThreadID

	-- Get current is announcement state of the thread
	IF EXISTS (	SELECT 
				ThreadID 
			FROM 
				tblForumThreads
			WHERE
				ThreadID = @ThreadID 
				AND IsSticky = 1
				AND StickyDate > DateAdd( y, 20, GetDate() )
			)

		SET @CurrentAnnouncementState = 1
	ELSE
		SET @CurrentAnnouncementState = 0
		

	-- Is the Post getting locked?
	IF @CurrentLockState != @IsLocked
	BEGIN
		UPDATE
			tblForumThreads
		SET
			IsLocked = @IsLocked
		WHERE
			ThreadID = @ThreadID

		UPDATE
			tblForumPosts
		SET
			IsLocked = @IsLocked
		WHERE
			ThreadID = @ThreadID

		IF @IsLocked = 0
		  exec spForumsystem_ModerationAction_AuditEntry 6, @ModeratorID, @ThreadID
        	ELSE
		  exec spForumsystem_ModerationAction_AuditEntry 5, @ModeratorID, @ThreadID
	END


	-- Is the post an Annoucement
	IF @CurrentAnnouncementState != @IsAnnouncement
		IF @IsAnnouncement = 1
		BEGIN
			UPDATE
				tblForumThreads
			SET
				IsSticky = 1,
				StickyDate = DateAdd(y, 25, ThreadDate)
			WHERE
				ThreadID = @ThreadID
	
			exec spForumsystem_ModerationAction_AuditEntry 16, @ModeratorID, @PostID
	
		END
		ELSE
		BEGIN
			UPDATE
				tblForumThreads
			SET
				IsSticky = 0,
				StickyDate = ThreadDate
			WHERE
				ThreadID = @ThreadID
	
			exec spForumsystem_ModerationAction_AuditEntry 17, @ModeratorID, @PostID
		END
END
ELSE
	print 'Toggling settings on a post.'

	-- Get the current lock state of the thread
	SELECT 
		@CurrentLockState = IsLocked
	FROM 
		tblForumPosts 
	WHERE 
		PostID = @PostID

	-- UPDATE The child posts
	UPDATE
	   	tblForumPosts
	SET
		IsLocked = @IsLocked
	WHERE
		ParentID = @PostID
	
	IF @IsLocked != @CurrentLockState
		IF @IsLocked = 0
		  exec spForumsystem_ModerationAction_AuditEntry 6, @ModeratorID, @PostID
	        ELSE
		  exec spForumsystem_ModerationAction_AuditEntry 5, @ModeratorID, @PostID
END



