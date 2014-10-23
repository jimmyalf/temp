CREATE PROCEDURE spContCrePgeLocLng
					@pgeId INT,
					@locId INT,
					@lngId INT,
					@isDefault BIT,
					@id INT OUTPUT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @pgeTpeId INT,
					@tmpLngId INT,
					@tmpId INT
					
					
			DECLARE chk_exs CURSOR LOCAL FOR
				SELECT	cId
				FROM	tblContPageLocationLanguage
				WHERE	cPgeId = @pgeId
					AND	cLocId = @locId
					AND	cLngId = @lngId
					
			OPEN chk_exs
			FETCH NEXT FROM chk_exs INTO @id
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_exs
					DEALLOCATE chk_exs
					
					SET @status = 0
					RETURN
				END
				
			CLOSE chk_exs
			DEALLOCATE chk_exs
								
			DECLARE get_type CURSOR LOCAL FOR
				SELECT	cPgeTpeId
				FROM	tblContPage
				WHERE	cId = @pgeId
								
			OPEN get_type
			FETCH NEXT FROM get_type INTO @pgeTpeId
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_type
					DEALLOCATE get_type
					SELECT @status = -1
				END
				
			CLOSE get_type
			DEALLOCATE get_type
							
			IF ((@pgeTpeId <> 2)
				AND (@pgeTpeId <> 3)
				AND (@pgeTpeId <> 4))
--				AND (@pgeTpeId <> 6)
--				AND (@pgeTpeId <> 7))
				BEGIN
					SELECT @status = -10
					SELECT @id = 0
					RETURN
				END
								
			INSERT INTO tblContPageLocationLanguage
				(cPgeId, cLocId, cLngId, cPgeTpeId, cIsDefault)
			VALUES
				(@pgeId, @locId, @lngId, @pgeTpeId, @isDefault)
				
			IF (@@ERROR = 0)
				BEGIN
					SELECT @id = @@IDENTITY
				END
			ELSE
				BEGIN
					SELECT @id = 0
					SELECT @status = @@ERROR
				END

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
									INSERT INTO tblContPageLocationLanguage
										(cPgeId, cLocId, cLngId, cPgeTpeId, 
										 cIsDefault)
									VALUES
										(@pgeId, @locId, @tmpLngId, @pgeTpeId, 
										 @isDefault)
										 
									SET @tmpId = @@IDENTITY
									
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
					IF (@pgeTpeId != 4)
						BEGIN
							UPDATE	tblContPageLocationLanguage
							SET		cIsDefault = 0
							WHERE	cLocId = @locId
								AND	cLngId = @lngId
								AND cPgeTpeId = @pgeTpeId
								AND	cId <> @id
						END
				END

			SELECT @status = @@ERROR

		END
