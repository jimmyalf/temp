CREATE procedure spForumGetForumMessages
(
	@MessageID int = 0
)
AS

IF @MessageID = 0
	SELECT 
		*
	FROM
		tblForumMessages
ELSE
	SELECT 
		*
	FROM
		tblForumMessages
	WHERE
		MessageID = @MessageID


