CREATE   procedure spForumUpdateEmailTemplate
(
	@EmailID		int,
	@Subject		nvarchar(256),
	@From			nvarchar(128),
	@Message		ntext
)
 AS
	-- Update a particular email message
	UPDATE Emails SET
		FromAddress = @From,
		Subject = @Subject,
		Message = @Message
	WHERE EmailID = @EmailID

