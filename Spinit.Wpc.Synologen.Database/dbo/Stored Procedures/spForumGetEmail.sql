CREATE procedure spForumGetEmail
(
	@EmailID	int = 0
)
AS
	IF @EmailID = 0
		SELECT
			*
		FROM 
			tblForumEmails (nolock)
		ORDER BY 
			Description
	ELSE
		SELECT
			*
		FROM 
			tblForumEmails (nolock)
		WHERE
			EmailID = @EmailID
		ORDER BY 
			Description



