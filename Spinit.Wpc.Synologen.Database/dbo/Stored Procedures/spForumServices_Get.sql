create proc spForumServices_Get
(
	@ServiceID int = 0
)
as 
	SELECT
		*
	FROM
		tblForumServices
	WHERE
		ServiceID = @ServiceID or
		(@ServiceID = 0 and 1=1)


