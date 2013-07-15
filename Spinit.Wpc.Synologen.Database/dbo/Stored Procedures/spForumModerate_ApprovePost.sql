CREATE         procedure spForumModerate_ApprovePost
(
	@PostID		int,
	@ApprovedBy	int
)
AS
DECLARE @ForumID 	int
DECLARE @ThreadID 	int
DECLARE @PostLevel 	int
DECLARE @UserID		int
DECLARE @IsLocked	bit

-- first make sure that the post is ALREADY non-approved
IF (SELECT IsApproved FROM tblForumPosts (nolock) WHERE PostID = @PostID) = 1
BEGIN
	print 'Post is already approved'
	SELECT 0
	RETURN
END
ELSE
BEGIN

	print 'Post is not approved'

	-- Get details about the thread and forum this post belongs in
	SELECT
		@ForumID = ForumID,
		@ThreadID = ThreadID,
		@PostLevel = PostLevel,
		@UserID	= UserID,
		@IsLocked = IsLocked
	FROM
		tblForumPosts
	WHERE
		PostID = @PostID

	-- Approve the post
	UPDATE 
		tblForumPosts
	SET 
		IsApproved = 1
	WHERE 
		PostID = @PostID

	-- Approved the thread if necessary
	IF @PostLevel = 1
		UPDATE
			tblForumThreads
		SET
			IsApproved = 1
		WHERE
			ThreadID = @ThreadID

	-- Update the user's post count
	exec spForumsystem_UpdateUserPostCount @ForumID, @UserID

	-- Update the forum statistics
	exec spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID

	-- Clean up unnecessary columns in forumsread
	exec spForumsystem_CleanForumsRead @ForumID

	-- update the thread stats
	exec spForumsystem_UpdateThread @ThreadID, @PostID

	-- Update Moderation audit table
	-- Update the ModerationAudit table
	exec spForumsystem_ModerationAction_AuditEntry 1, @ApprovedBy, @PostID

	-- Send back a success code
	SELECT 1
	
END


