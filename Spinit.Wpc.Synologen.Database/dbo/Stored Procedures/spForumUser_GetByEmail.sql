create procedure spForumUser_GetByEmail
(
	@Email		nvarchar(64)
)
AS
SELECT 
	UserID
FROM
	tblForumUsers
WHERE
	Email = @Email

