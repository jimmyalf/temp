CREATE PROCEDURE spBaseDisconnectLocationAndLanguage
@locationid INT, @langid INT, @status INT OUTPUT
AS
BEGIN
			DECLARE @isDefault BIT
			
			DECLARE get_default CURSOR LOCAL FOR
				SELECT	cIsDefault
				FROM	tblBaseLocationsLanguages	
				WHERE	cLocationId = @locationid 
				AND		cLanguageId = @langid
				
			OPEN get_default
			FETCH NEXT FROM get_default INTO @isDefault
			
			
			IF (@@FETCH_STATUS <> -1) 
				BEGIN
					DELETE FROM tblBaseLocationsLanguages
					WHERE	cLocationId = @locationid
						AND	cLanguageId = @langid
					
					IF (@isDefault = 1)
						BEGIN
							UPDATE	tblBaseLocationsLanguages
							SET		cIsDefault = 0
							WHERE	cLocationId = @locationId
								AND	cLanguageId IN (SELECT	MIN (cLanguageId)
													FROM	tblBaseLocationsLanguages
													WHERE	cLocationId = @locationId)
						END
						
					UPDATE	tblContTree
					SET		cPublishDate = NULL
					WHERE	cLocId = @locationId
				END
				
			CLOSE get_default
			DEALLOCATE get_default

			IF (@@ERROR = 0)
				BEGIN
					SELECT @status = 0
				END
			ELSE
				BEGIN
					SELECT @status = @@ERROR
				END
	END
