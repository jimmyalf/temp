CREATE        PROCEDURE spForumModerate_DeletePost
(
	@PostID INT,
	@DeletedBy INT,
	@Reason NVARCHAR(1024),
	@DeleteChildPosts BIT = 1
)
AS

DECLARE @IsApproved bit

-- First, get information about the post that is about to be deleted.
SELECT
    @IsApproved = IsApproved
FROM
    tblForumPosts
WHERE
    PostID = @PostID

-- If the post is not approved, permanently delete the post, otherwise, execute tblForumPost_Delete
IF @IsApproved = 0
BEGIN
	
    -- Delete the post.
    DELETE FROM tblForumPosts
    WHERE PostID = @PostID

END	
ELSE
    EXEC spForumsystem_DeletePostAndChildren @PostID = @PostID, @DeleteChildren = @DeleteChildPosts

-- Update Moderation Audit table
exec spForumsystem_ModerationAction_AuditEntry 4, @DeletedBy, @PostID, null, null, @Reason



