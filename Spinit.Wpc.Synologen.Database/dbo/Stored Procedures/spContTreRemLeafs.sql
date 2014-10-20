CREATE PROCEDURE spContTreRemLeafs
					@id INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @leafId INT,
					@statusRet INT,
					@orderNo INT,
					@parent INT,
					@pgeId INT,
					@crsTpeId INT,
					@treTpeId INT,
					@crsId INT
									
			SELECT	@pgeId = cPgeId,
					@crsTpeId = cCrsTpeId,
					@treTpeId = cTreTpeId
			FROM	tblContTree
			WHERE	cId = @id

			-- Examine if there are any childs to remove 
			DECLARE get_leafs CURSOR LOCAL FOR 
				SELECT	cId 
				FROM	tblContTree
				WHERE	cParent = @id
				
			SELECT @status = 0
							
			OPEN get_leafs
			FETCH NEXT FROM get_leafs INTO @leafId
					
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN

					-- Recursive call					
					EXECUTE spContTreRemLeafs @leafId, @statusRet OUTPUT
					
					IF (@statusRet <> 0)
						BEGIN
							SELECT @status = @statusRet
							CLOSE get_leafs
							DEALLOCATE get_leafs
							RETURN
						END
					
					FETCH NEXT FROM get_leafs INTO @leafId
				END
			
			CLOSE get_leafs
			DEALLOCATE get_leafs
						
			-- Examine if the node is crosspublished, master or slave
			IF (@crsTpeId <> 1)
			BEGIN
				EXECUTE spContTreRemCPNode @Id, @statusRet OUTPUT
				IF (@statusRet <> 0)
				BEGIN
					SELECT @status = @statusRet
					RETURN
				END		
			END
			
			-- Examine if the node is connected to a page
			IF (@pgeId IS NOT NULL)
			BEGIN
				EXECUTE spContPgeRemPage @pgeId, @statusRet OUTPUT
				IF (@statusRet <> 0)
				BEGIN
					SELECT @status = @statusRet
					RETURN
				END		
				IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR 
					RETURN
				END		
			END	
			
			-- Perform delete on the node if it's not the trashcan
			IF (@treTpeId <> 10)
			BEGIN							
				DELETE FROM	tblContTreeBaseGroups
				WHERE		cTreId = @id
				
				DELETE FROM tblContTree
				WHERE		cId = @id
				
			END
		
			SELECT @status = @@ERROR
		END
