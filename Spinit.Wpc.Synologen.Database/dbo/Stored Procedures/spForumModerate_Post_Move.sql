CREATE    PROCEDURE spForumModerate_Post_Move
(
    @PostID int,
    @MoveToForumID int,
    @MovedBy int
)
AS

DECLARE @ThreadID INT
DECLARE @ForumID INT
DECLARE @IsApproved BIT
DECLARE @PostLevel INT
DECLARE @Notes NVARCHAR(1024)

-- First, get information about the post that is about to be moved.
SELECT
	@ThreadID = ThreadID,
	@ForumID = ForumID,
	@PostLevel = PostLevel,
	@IsApproved = IsApproved
FROM
	tblForumPosts
WHERE
	PostID = @PostID

-- EAD: We only move the post if it is a top level post.
IF @PostLevel = 1
BEGIN
	DECLARE @MoveToIsModerated SMALLINT
	
	-- Get information about the destination forum.
	SELECT 
		@MoveToIsModerated = IsModerated
	FROM 
		tblForumForums
	WHERE 
		ForumID = @MoveToForumID
	
	-- If the post is not already approved, check the moderation status and permissions on the moderator for approved status.
	IF @IsApproved = 0
	BEGIN
		-- If the destination forum requires moderation, make sure the moderator has permission.
		IF @MoveToIsModerated = 1
		BEGIN
			IF EXISTS(
				SELECT
					UserID
				FROM
					tblForumUsersInRoles
				WHERE
					UserID = @MovedBy
					AND RoleID IN (
						SELECT 
							RoleID 
						FROM 
							tblForumForumPermissions
						WHERE 
							ForumID = @ForumID
							AND Moderate = 1 
					)
			)
			BEGIN
				-- The moderator has permissions to move the post and approve it.		
				UPDATE
					tblForumPosts
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				UPDATE
					tblForumThreads
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				-- approve the post
				EXEC spForumModerate_ApprovePost @PostID, @MovedBy
				
				SET @Notes = 'The post was moved and approved.'
				PRINT @Notes
				SELECT 2
			END
			ELSE BEGIN
				-- The moderator has permissions to move the post but not approve.			
				UPDATE
					tblForumPosts
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				UPDATE
					tblForumThreads
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID
				
				SET @Notes = 'The post was moved but not approved.'
				PRINT @Notes
				SELECT 1
			END
		END
		ELSE BEGIN
			UPDATE
				tblForumPosts
			SET 
				ForumID = @MoveToForumID
			WHERE 
				ThreadID = @ThreadID

			UPDATE
				tblForumThreads
			SET 
				ForumID = @MoveToForumID
			WHERE 
				ThreadID = @ThreadID

			-- The destination forum is not moderated, approve the post and move the post.
			EXEC spForumModerate_ApprovePost @PostID, @MovedBy
			
			SET @Notes = 'The post was moved and approved.'
			PRINT @Notes
			SELECT 2
		END
	END
	ELSE BEGIN
		-- The post is already approved, move the post.
		UPDATE
			tblForumPosts
		SET 
			ForumID = @MoveToForumID
		WHERE 
			ThreadID = @ThreadID

		UPDATE
			tblForumThreads
		SET 
			ForumID = @MoveToForumID
		WHERE 
			ThreadID = @ThreadID
		
		print 'The approved post was moved.'
		SET @Notes = 'The approved post was moved.'
		SELECT 3
	
	END

	-- Reset the statistics on both forums.
	EXEC spForumsystem_ResetForumStatistics @ForumID
	EXEC spForumsystem_ResetForumStatistics @MoveToForumID
	
	-- Reset the thread statistics on the moved thread.
	EXEC spForumsystem_ResetThreadStatistics @ThreadID
		
	-- Update Moderation Audit table
	EXEC spForumsystem_ModerationAction_AuditEntry 3, @MovedBy, @PostID, null, @ForumID, @Notes

END
ELSE BEGIN
	print 'The post was not moved.'
	SELECT 0
END




