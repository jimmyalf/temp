CREATE         PROCEDURE spForumGetUserIDByAppToken
(
	@AppUserToken varchar(128)
)
AS

SELECT
	U.UserID
FROM 
	tblForumUsers U (nolock)
WHERE 
	U.AppUserToken = @AppUserToken




