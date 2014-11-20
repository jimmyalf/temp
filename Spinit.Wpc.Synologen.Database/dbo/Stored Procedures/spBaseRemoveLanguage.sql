create PROCEDURE spBaseRemoveLanguage
					@id INT,
					@status INT OUTPUT
	AS
		DELETE From tblBaseLocationsLanguages
		WHERE cLanguageId = @id
	
		DELETE FROM tblBaseLanguages
		WHERE cId = @id
			
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
