
CREATE PROCEDURE spContTreChgNodePge
					@id INT,
					@pgeId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @treTpeId INT
			
			DECLARE get_tpe CURSOR LOCAL FOR
				SELECT	cTreTpeId
				FROM	tblContTree
				WHERE	cId = @id
				
			OPEN get_tpe
			FETCH NEXT FROM get_tpe INTO @treTpeId
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_tpe
					DEALLOCATE get_tpe
					
					SELECT @status = -2
					RETURN
				END
				
			CLOSE get_tpe
			DEALLOCATE get_tpe
			
			IF ((@treTpeId = 4) OR (@treTpeId = 5)
				OR (@treTpeId = 7) OR (@treTpeId = 8))
				BEGIN
					UPDATE	tblContTree
					SET		cPgeId = @pgeId
					WHERE	cId = @id
				END
			ELSE
				BEGIN
					SELECT @status = -10
					RETURN
				END
			
			SELECT @status = @@ERROR
		END
