create procedure spBaseChangeLocationAndLanguage
@locationid INT,
					@langid INT,
					@isDefault BIT,
					@status INT OUTPUT
	AS

			BEGIN	
			Select * From tblBaseLocationsLanguages	
			Where cLocationId = @locationid and cLanguageId = @langid
			
			if (@@ROWCOUNT <> 0) 
				BEGIN
					UPDATE	tblBaseLocationsLanguages
					SET		cIsDefault = @isDefault
					WHERE	cLocationId = @locationid
						AND	cLanguageId = @langid
						
					IF (@isDefault = 1)
						BEGIN
							UPDATE	tblBaseLocationsLanguages
							SET		cIsDefault = 0
							WHERE	cLocationId = @locationid
								AND	cLanguageId <> @langid
						END

					UPDATE	tblContTree
					SET		cPublishDate = NULL
					WHERE	cLocId = @locationId
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
