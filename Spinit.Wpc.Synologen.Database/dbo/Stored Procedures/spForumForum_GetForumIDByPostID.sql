CREATE PROCEDURE spForumForum_GetForumIDByPostID
(
	@UserID			int = 0,
	@PostID			int = 0
)
AS

-- Loop up the forum by PostID
DECLARE @ForumID int
SET @ForumID = (SELECT ForumID FROM tblForumPosts WHERE PostID = @PostID)

SELECT @ForumID


