
CREATE PROCEDURE spNewsDisconnectFromLanguage
					@newsId INT,
					@languageId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DELETE FROM tblNewsLanguageConnection
			WHERE cNewsId = @newsId AND cLanguageId = @languageId
		END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
