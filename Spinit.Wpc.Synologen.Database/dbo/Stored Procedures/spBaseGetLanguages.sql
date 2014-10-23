

CREATE PROCEDURE spBaseGetLanguages
					@type INT,
					@languageid INT,
					@locationid INT,
					@status INT OUTPUT
					
	AS
		IF (@type = 0)
			BEGIN
				SELECT	*
				FROM	tblBaseLanguages
				WHERE	cId = @languageid
			END
		IF (@type = 1)
			BEGIN
				SELECT	*
				FROM	tblBaseLanguages
			END
		IF (@type = 2)
			BEGIN
				SELECT	*
				FROM	tblBaseLanguages, tblBaseLocationsLanguages
				WHERE cLocationId = @locationid
				AND cLanguageId = cId
			END
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END


