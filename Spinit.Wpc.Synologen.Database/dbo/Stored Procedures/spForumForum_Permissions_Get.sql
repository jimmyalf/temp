create procedure spForumForum_Permissions_Get
(
	@ForumID int
)
AS
SELECT 
	R.Name,
	P.* 
FROM 
	tblForumForumPermissions P, 
	tblForumRoles R 
WHERE 
	P.RoleID = R.RoleID AND
	P.ForumID = @ForumID





