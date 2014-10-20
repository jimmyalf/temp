CREATE              PROCEDURE spForum_ForumsGet
(
	@SiteID			int = 0,
	@UserID			int = 0
)
AS

-- Return all the forums
SELECT
	MostRecentPostAuthor = dbo.sfForumGetUserDisplayName(F.MostRecentPostAuthorID),
	F.*,
	LastUserActivity = (CASE @UserID
		WHEN 0 THEN '1/1/1797'
		ELSE ( ISNULL( (SELECT LastActivity FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = F.ForumID), '1/1/1797'))
        END)
FROM
	tblForumForums F
WHERE
	(SiteID = @SiteID OR SiteID = 0) AND
	IsActive = 1


-- Return permissions for this user
SELECT
	P.*
FROM
	tblForumForumPermissions P,
	tblForumUsersInRoles R
WHERE
	P.RoleID = R.RoleID AND
	(R.UserID = @UserID OR R.UserID = 0)
