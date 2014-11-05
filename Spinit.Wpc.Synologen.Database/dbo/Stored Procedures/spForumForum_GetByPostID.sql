CREATE PROCEDURE spForumForum_GetByPostID
(
	@UserID			int = 0,
	@PostID			int = 0
)
AS

-- Loop up the forum by PostID
DECLARE @ForumID int
SET @ForumID = (SELECT ForumID FROM tblForumPosts WHERE PostID = @PostID)

SELECT @ForumID

-- Return all the forums
SELECT
	F.*,
	LastUserActivity = ISNULL((SELECT LastActivity FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = F.ForumID), '1/1/1797')
FROM
	tblForumForums F
WHERE
	ForumID = @ForumID


-- Return permissions for this user
SELECT
	P.*
FROM
	tblForumForumPermissions P,
	tblForumUsersInRoles R
WHERE
	P.RoleID = R.RoleID AND
	(R.UserID = @UserID OR R.UserID = 0) AND
	P.ForumID = @ForumID


