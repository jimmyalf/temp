create FUNCTION sfForumGetUserHasViewRightsOnForum (@userName NVARCHAR(64), @forumID INT)
	RETURNS BIT
AS BEGIN
	DECLARE @returnValue BIT

IF EXISTS(
	SELECT tblForumUsers.UserID
	FROM tblForumUsers
		INNER JOIN tblForumUsersInRoles 
			ON tblForumUsersInRoles.UserID = tblForumUsers.UserID
		INNER JOIN tblForumForumPermissions 
			ON tblForumForumPermissions.RoleID = tblForumUsersInRoles.RoleID 
				OR tblForumForumPermissions.RoleID = 0
	WHERE UserName = @userName
		AND tblForumForumPermissions.ForumID = @forumID
		AND tblForumForumPermissions.[View] = 1 ) BEGIN
		SET @returnValue = 1
	END
	ELSE BEGIN
		SET @returnValue = 0
	END
	
	RETURN @returnValue
END
