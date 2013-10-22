create PROCEDURE spQuickFormAddUpdateDeleteQuickForm
					@type INT,
					@id INT OUTPUT,
					@formType INT = '-1',
					@name NVARCHAR(255) = '',
					@content NTEXT = '',
					@subject NVARCHAR(255) = '',
					@mailTo NVARCHAR(1000) = '',
					@mailFromDefault NVARCHAR(255) = '',
					@returnUrl NVARCHAR(255) = '',
					@comitText NVARCHAR(255) = '',
					@isEmailConfirmationActivated BIT = '1', --false
					@emailConfirmationBody NTEXT = '',
					@status INT OUTPUT
	AS
	IF (@type = 0) -- create
	BEGIN
		INSERT INTO tblQuickForm
			(cFormType, cName, cContent, cMailSubject, cMailTo, cMailFromDefault, cReturnUrl, cComitText, cConfirmationEmailActivated, cEmailConfirmationBody)
		VALUES
			(@formType, @name, @content, @subject, @mailTo, @mailFromDefault, @returnUrl, @comitText, @isEmailConfirmationActivated, @emailConfirmationBody)
		SELECT @id = @@IDENTITY
	END
	IF (@type = 1) -- update
	BEGIN
		UPDATE tblQuickForm
		SET cFormType = @formType,
		cName = @name,
		cContent = @content,
		cMailSubject = @subject,
		cMailTo = @mailTo,
		cMailFromDefault = @mailFromDefault,
		cReturnUrl = @returnUrl,
		cComitText = @comitText,
		cConfirmationEmailActivated = @isEmailConfirmationActivated,
		cEmailConfirmationBody = @emailConfirmationBody
		WHERE cId = @id
	END
	IF (@type = 2) -- delete
	BEGIN
		SELECT cId 
		FROM tblQuickFormInbox
		WHERE cQuickFormId = @id
		IF (@@ROWCOUNT = 0)
		BEGIN
			DELETE FROM tblQuickForm
			WHERE cId = @id
		END
		ELSE
		BEGIN
			SELECT @status = -1
			RETURN
		END
	END	
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
