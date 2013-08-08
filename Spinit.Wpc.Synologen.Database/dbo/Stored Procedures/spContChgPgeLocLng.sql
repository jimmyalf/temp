CREATE PROCEDURE spContChgPgeLocLng
					@id INT,
					@isDefault BIT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @pgeId INT,
					@locId INT,
					@lngId INT,
					@pgeIsDefault INT,
					@pgeTpeId INT,
					@tmpLngId INT,
					@tmpId INT					
					
			DECLARE get_locLng CURSOR LOCAL FOR
				SELECT	cPgeId,
						cLocId,
						cLngId,
						cIsDefault,
						cPgeTpeId
				FROM	tblContPageLocationLanguage
				WHERE	cId = @id
									
			OPEN get_locLng
			FETCH NEXT FROM get_locLng INTO	@pgeId,
											@locId,
											@lngId,
											@pgeIsDefault,
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
								
			IF (@isDefault IS NOT NULL)
				BEGIN
					SELECT @pgeIsDefault = @isDefault
				END
				
			UPDATE	tblContPageLocationLanguage
			SET		cIsDefault = @pgeIsDefault
			WHERE	cId = @id

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
								
									UPDATE	tblContPageLocationLanguage
									SET		cIsDefault = @pgeIsDefault
									WHERE	cId = @tmpId
										 									
									IF (@isDefault = 1)
										BEGIN
											UPDATE	tblContPageLocationLanguage
											SET		cIsDefault = 0
											WHERE	cLocId = @locId
												AND	cLngId = @tmpLngId
												AND cPgeTpeId = @pgeTpeId
												AND	cId <> @tmpId
										END
								END
								
							FETCH NEXT FROM getLanguages INTO @tmpLngId
						END
						
					CLOSE getLanguages
					DEALLOCATE getLanguages						
				END

			IF (@isDefault = 1)
				BEGIN
					UPDATE	tblContPageLocationLanguage
					SET		cIsDefault = 0
					WHERE	cLocId = @locId
						AND	cLngId = @lngId
						AND	cPgeTpeId = @pgeTpeId
						AND	cId <> @id
				END

			SELECT @status = @@ERROR
		END
