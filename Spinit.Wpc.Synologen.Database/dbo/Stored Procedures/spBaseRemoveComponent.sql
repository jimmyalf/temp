CREATE PROCEDURE spBaseRemoveComponent
					@id INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@dummy INT
			
			DECLARE	chk_exist CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseComponents
				WHERE	cId = @id
				
			OPEN chk_exist
			FETCH NEXT FROM chk_exist INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE chk_exist
					DEALLOCATE chk_exist
					
					SET @status = -2
					RETURN
				END
				
			CLOSE chk_exist
			DEALLOCATE chk_exist
				
			DELETE FROM tblBaseExternalComponents
			WHERE		cComponentId = @id
			
			DELETE FROM tblBaseLocationsComponents
			WHERE		cComponentId = @id
			
			DELETE FROM tblBaseComponents
			WHERE		cId = @id
			
			SET @status = @@ERROR		
		END
