create procedure spForumMessage_CreateUpdateDelete
(
	@MessageID int,
	@Title NVarChar(1024),
	@Body NVarChar(4000),
	@Action int
)
AS
-- CREATE
IF @Action = 0
BEGIN
	SELECT 'Not Implemented'
END
-- UPDATE
ELSE IF @Action  = 1
BEGIN
	UPDATE
		tblForumMessages
	SET
		Title = @Title,
		Body = @Body
	WHERE
		MessageID = @MessageID
END

-- DELETE
ELSE IF @Action = 2
BEGIN
	SELECT 'Not Implemented'
END	

