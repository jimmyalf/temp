create procedure spBaseGetLocationAndLanguage
@locationid INT,
					@langid INT,
					@status INT OUTPUT
	AS

		BEGIN	
			IF ((@locationid IS NULL) AND (@langid IS NULL))
				BEGIN
					SELECT	*
					FROM	tblBaseLocationsLanguages
				END
				
			IF ((@locationid IS NOT NULL) AND (@langid IS NULL))
				BEGIN
					SELECT	*
					FROM	tblBaseLocationsLanguages
					WHERE	cLocationId = @locationid
				END
			
			IF ((@locationid IS NULL) AND (@langid IS NOT NULL))
				BEGIN
					SELECT	*
					FROM	tblBaseLocationsLanguages
					WHERE	cLanguageId = @langid
				END
			
			IF ((@locationid IS NOT NULL) AND (@langid IS NOT NULL))
				BEGIN
					SELECT	*
					FROM	tblBaseLocationsLanguages
					WHERE	cLocationId = @locationid
						AND cLanguageId = @langid
				END
				
			SET @status = @@ERROR
		END
