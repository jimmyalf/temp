CREATE PROCEDURE spContRemPgeLocLng
@id INT, @status INT OUTPUT
AS
BEGIN
			DECLARE @pgeId INT,
					@locId INT,
					@lngId INT,
					@pgeTpeId INT,
					@tmpLngId INT,
					@tmpId INT					
					
			DECLARE get_locLng CURSOR LOCAL FOR
				SELECT	cPgeId,
						cLocId,
						cLngId,
						cPgeTpeId
				FROM	tblContPageLocationLanguage
				WHERE	cId = @id
									
			OPEN get_locLng
			FETCH NEXT FROM get_locLng INTO	@pgeId,
											@locId,
											@lngId,
											@pgeTpeId
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_locLng
					DEALLOCATE get_locLng
					
					SELECT @status = -3
					RETURN
				END
				
			CLOSE get_locLng
			DEALLOCATE get_locLng

			DELETE FROM	tblContPageLocationLanguage
			WHERE		cId = @id
			
			-- Always put stylesheet on all languages
			IF (@pgeTpeId = 4)
				BEGIN
					DECLARE getLanguages CURSOR LOCAL FOR
						SELECT	cLanguageId
						FROM	tblBaseLocationsLanguages
						WHERE	cLocationId = @locId
						
					OPEN getLanguages
					FETCH NEXT FROM getLanguages INTO @tmpLngId
					
					WHILE (@@FETCH_STATUS != -1)
						BEGIN
							IF (@tmpLngId != @lngId)
								BEGIN
									DECLARE getPageCon CURSOR LOCAL FOR
										SELECT	cId
										FROM	tblContPageLocationLanguage
										WHERE	cPgeId = @pgeId
											AND	cLocId = @locId
											AND	cLngId = @tmpLngId
											
									OPEN getPageCon
									FETCH NEXT FROM getPageCon INTO @tmpId
									
									IF (@@FETCH_STATUS = -1)
										BEGIN
											CLOSE getPageCon
											DEALLOCATE getPageCon
											
											FETCH NEXT FROM getLanguages INTO @tmpLngId
											CONTINUE
										END
										
									CLOSE getPageCon
									DEALLOCATE getPageCon
								
									DELETE FROM	tblContPageLocationLanguage
									WHERE		cId = @tmpId
									
								END
									 																
							FETCH NEXT FROM getLanguages INTO @tmpLngId
						END
						
					CLOSE getLanguages
					DEALLOCATE getLanguages
				END
				
			SELECT @status = @@ERROR
		END
