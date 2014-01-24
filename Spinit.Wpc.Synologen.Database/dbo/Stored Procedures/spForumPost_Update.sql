CREATE           PROCEDURE spForumPost_Update
(
	@PostID	int,
	@Subject	nvarchar(256),
	@Body		ntext,
	@FormattedBody	ntext,
	@EmoticonID	int = 0,
	@IsSticky 	bit = null,
	@StickyDate 	datetime = null,
	@IsLocked	bit,
	@EditedBy	int,
	@EditNotes	ntext = null
)
AS
	DECLARE @ThreadID int
	DECLARE @PostDate datetime

	-- this sproc updates a post (called from the moderate/admin page)
	UPDATE 
		tblForumPosts 
	SET
		Subject = @Subject,
--		PostDate = GetDate(),	-- This does not get updated.  the original date IS the date!
		Body = @Body,
		FormattedBody = @FormattedBody,
		IsLocked = @IsLocked,
		EmoticonID = @EmoticonID
	WHERE 
		PostID = @PostID

	-- Allow thread to update sticky properties.
	IF (@IsSticky IS NOT NULL) AND (@StickyDate IS NOT NULL)
	BEGIN
		-- Get the thread and postdate this applies to
		SELECT 
			@ThreadID = ThreadID,
			@PostDate = PostDate 
		FROM 
			tblForumPosts 
		WHERE 
			PostID = @PostID

		IF (@StickyDate > '1/1/2000')
		BEGIN
			-- valid date range given
			UPDATE
				tblForumThreads
			SET
				IsSticky = @IsSticky,
				StickyDate = @StickyDate
			WHERE 
				ThreadID = @ThreadID 
		END
		ELSE BEGIN
			-- trying to remove a sticky
			UPDATE
				tblForumThreads
			SET
				IsSticky = @IsSticky,
				StickyDate = @PostDate
			WHERE 
				ThreadID = @ThreadID 		
		END
	END

	IF @EditNotes IS NOT NULL
		IF EXISTS (SELECT PostID FROM tblForumPostEditNotes WHERE PostID = @PostID)
			UPDATE 
				tblForumPostEditNotes
			SET
				EditNotes = @EditNotes
			WHERE	
				PostID = @PostID
		ELSE
			INSERT INTO
				tblForumPostEditNotes
			VALUES
				(@PostID, @EditNotes)

	-- We want to track what happened
	exec spForumsystem_ModerationAction_AuditEntry 2, @EditedBy, @PostID, NULL, NULL, @EditNotes



