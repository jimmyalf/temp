create PROCEDURE spQuickFormMarkAllAsRead
					@id INT,
					@status INT OUTPUT
	AS
	BEGIN
		UPDATE tblQuickFormInbox
		SET cRead = 1
		WHERE cQuickFormId = @id
	END
	
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
