create                PROCEDURE spForumGetMessage
(
	@MessageID int
)
 AS
BEGIN
	SELECT
		Title,
		Body
	FROM
		Messages
	WHERE
		MessageID = @MessageID
END




