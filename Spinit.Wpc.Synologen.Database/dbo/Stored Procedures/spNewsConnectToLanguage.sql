
CREATE PROCEDURE spNewsConnectToLanguage
					@newsId INT,
					@languageId INT,
					@status INT OUTPUT
	AS
	BEGIN	
		INSERT INTO tblNewsLanguageConnection
			(cNewsId, cLanguageId)
		VALUES
			(@newsId, @languageId)		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
