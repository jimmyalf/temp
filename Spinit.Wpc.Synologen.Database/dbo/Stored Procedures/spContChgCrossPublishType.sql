
CREATE PROCEDURE spContChgCrossPublishType
					@id INT,
					@name NVARCHAR (50),
					@description NVARCHAR (50),
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @tpeName NVARCHAR (50),
					@tpeDescription NVARCHAR (256)
					
			DECLARE get_crsTpe CURSOR LOCAL FOR
				SELECT	cName,
						cDescription
				FROM	tblContCrossPublishType
				WHERE	cId = @id
				
			OPEN get_crsTpe
			FETCH NEXT FROM get_crsTpe INTO	@tpeName,
											@tpeDescription
											
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_crsTpe
					DEALLOCATE get_crsTpe
					
					SELECT @status = -2
					RETURN
				END
			
			CLOSE get_crsTpe
			DEALLOCATE get_crsTpe
			
			IF (@name IS NOT NULL)
				BEGIN
					IF (LEN (@name) = 0)
						BEGIN
							SELECT @tpeName = NULL
						END
					ELSE
						BEGIN
							SELECT @tpeName = @name
						END
				END
				
			IF (@description IS NOT NULL)
				BEGIN
					IF (LEN (@description) = 0)
						BEGIN
							SELECT @tpeDescription = NULL
						END
					ELSE
						BEGIN
							SELECT @tpeDescription = @description
						END
				END
				
			UPDATE	tblContCrossPublishType
			SET		cName = @name,
					cDescription = @description
			WHERE	cId = @id
		
			SELECT @status = @@ERROR
		END
