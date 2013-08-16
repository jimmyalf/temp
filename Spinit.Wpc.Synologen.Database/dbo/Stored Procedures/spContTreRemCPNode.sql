
CREATE PROCEDURE spContTreRemCPNode
					@id INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @treId INT,
					@treCrsId INT,
					@newPgeId INT
							
			DECLARE chk_cross1 CURSOR LOCAL FOR
				SELECT	cTreId, cTreCrsId
				FROM	tblContCrossPublish
				WHERE	(cTreId = @Id)
				
			OPEN chk_cross1
			FETCH NEXT FROM chk_cross1 INTO @treId, @treCrsId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN		
				
					-- Remove crosspublish	
				
					DELETE FROM	tblContCrossPublish
					WHERE ((cTreCrsId = @treCrsId) AND (cTreId = @treId))
					
					-- Update crosspublish type to normal for connected tree
					
					UPDATE tblContTree
					SET cCrsTpeId = 1
					WHERE cId = @treCrsId
		
					FETCH NEXT FROM chk_cross1 INTO @treId, @treCrsId
				END
				
			CLOSE chk_cross1
			DEALLOCATE chk_cross1
			
			DECLARE chk_cross2 CURSOR LOCAL FOR
				SELECT	cTreId, cTreCrsId
				FROM	tblContCrossPublish
				WHERE	(cTreCrsId = @id)
				
			OPEN chk_cross2
			FETCH NEXT FROM chk_cross2 INTO @treId, @treCrsId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
				
					-- Remove crosspublish	
				
					DELETE FROM	tblContCrossPublish
					WHERE ((cTreCrsId = @treCrsId) AND (cTreId = @treId))
					
					-- Update crosspublish type to normal for connected tree
					
					UPDATE tblContTree
					SET cCrsTpeId = 1
					WHERE cId = @treId

					FETCH NEXT FROM chk_cross2 INTO @treId, @treCrsId
				END
				
			CLOSE chk_cross2
			DEALLOCATE chk_cross2
			
			
			SELECT @status = @@ERROR
							
		END
