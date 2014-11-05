CREATE PROCEDURE spBaseConnectLocationAndLanguage
					@locationid INT,
					@langid INT,
					@isDefault BIT,
					@status INT OUTPUT
	AS

			BEGIN	
			Select * From tblBaseLocationsLanguages	
			Where cLocationId = @locationid and cLanguageId = @langid
			
			if (@@ROWCOUNT = 0) 
				BEGIN
					IF (@isDefault = 1)
						BEGIN
							UPDATE	tblBaseLocationsLanguages
							SET		cIsDefault = 0
							WHERE	cLocationId = @locationId
						END

					insert into tblBaseLocationsLanguages
					(cLocationId,cLanguageId, cIsDefault)
					Values
					(@locationid, @langid, @isDefault)
					

					UPDATE	tblContTree
					SET		cPublishDate = NULL
					WHERE	cLocId = @locationId
				END		
			
			IF (@@ERROR = 0)
				BEGIN
					SELECT @status = 0
				END
			ELSE
				BEGIN
					SELECT @status = @@ERROR
				END
	END
