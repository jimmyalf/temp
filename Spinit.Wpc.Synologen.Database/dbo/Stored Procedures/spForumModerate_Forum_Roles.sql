create procedure spForumModerate_Forum_Roles
(
	@ForumID	int
)
AS
SELECT 
	R.RoleID,
	[Name],
	Description 
FROM 
	tblForumForumPermissions P,
	tblForumRoles R
WHERE
	P.RoleID = R.RoleID AND
	Moderate = 1 AND
	ForumID = @ForumID


