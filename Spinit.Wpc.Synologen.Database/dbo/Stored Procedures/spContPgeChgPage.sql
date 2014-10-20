CREATE PROCEDURE spContPgeChgPage
					@id INT,
					@name NVARCHAR (255),
					@changedBy NVARCHAR (100),
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@dummy INT
					
			DECLARE get_page CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContPage
				WHERE	cId = @id
				
			OPEN get_page
			FETCH NEXT FROM get_page INTO	@dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_page
					DEALLOCATE get_page
					
					SELECT @status = -3
					RETURN
				END
				
			CLOSE get_page
			DEALLOCATE get_page
									
			SET @status = 0
																		
			UPDATE	tblContPage
			SET		cName = @name,
					cChangedBy = @changedBy,
					cChangedDate = GETDATE ()
			WHERE	cId = @id
				
			IF (@status = 0)
				BEGIN
					SET @status = @@ERROR
				END
		END
