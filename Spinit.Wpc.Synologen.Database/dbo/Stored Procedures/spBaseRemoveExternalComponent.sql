CREATE PROCEDURE spBaseRemoveExternalComponent
@componentId INT, @status INT OUTPUT
AS
BEGIN
			DECLARE @dummy INT
			
			DECLARE chk_exist CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseExternalComponents
				WHERE	cComponentId = @componentId
				
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
			
			DELETE FROM	tblBaseExternalComponents
			WHERE		cComponentId = @componentId
			
			UPDATE	tblBaseComponents
			SET		cExternal = 0
			WHERE	cId = @componentId
			
			SET @status = @@ERROR
		END
