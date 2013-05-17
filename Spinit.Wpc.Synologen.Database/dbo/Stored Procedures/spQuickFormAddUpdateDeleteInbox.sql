create PROCEDURE spQuickFormAddUpdateDeleteInbox
					@type INT,
					@id INT OUTPUT,
					@quickFormId INT = -1,
					@read BIT = 0,
					@content NTEXT = '',
					@from NVARCHAR(255) = '',
					@status INT OUTPUT
	AS
	IF (@type = 0) -- create
	BEGIN
		INSERT INTO tblQuickFormInbox
			(cQuickFormId, cRead, cContent, cFrom, cSubmitDate)
		VALUES
			(@quickFormId, @read, @content, @from, getDate())
		SELECT @id = @@IDENTITY
	END
	IF (@type = 1) -- update
	BEGIN
		UPDATE tblQuickFormInbox
		SET cQuickFormId = @quickFormId,
		cRead = @read,
		cContent = @content,
		cFrom = @from
		WHERE cId = @id
	END
	IF (@type = 2) -- delete
	BEGIN
		DELETE FROM tblQuickFormInbox
		WHERE cId = @id
	END	
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
