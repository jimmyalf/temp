
CREATE PROCEDURE spContRemCrossPublish
					@treId INT,
					@treCrsId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@count INT
			
			DECLARE get_masterCount CURSOR LOCAL FOR
				SELECT	COUNT (cTreCrsId)
				FROM	tblContCrossPublish
				WHERE	cTreId = @treId
				
			OPEN get_masterCount
			FETCH NEXT FROM get_masterCount INTO @count
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_masterCount
					DEALLOCATE get_masterCount
					
					SELECT @status = -2
					RETURN
				END
				
			CLOSE get_masterCount
			DEALLOCATE get_masterCount
		
			DELETE FROM	tblContCrossPublish
			WHERE		cTreId = @treId
				AND		cTreCrsId = @treCrsId
				
			UPDATE	tblContTree
			SET		cCrsTpeId = 1
			WHERE	cId = @treCrsId
			
			IF (@count < 2)
				BEGIN
					UPDATE	tblContTree
					SET		cCrsTpeId = 1
					WHERE	cId = @treId
				END
			
			SELECT @status = @@ERROR
		END
