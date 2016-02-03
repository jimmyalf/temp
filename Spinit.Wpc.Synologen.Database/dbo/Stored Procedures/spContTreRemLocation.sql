CREATE PROCEDURE spContTreRemLocation
					@id INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @parent INT,
					@pgeId INT,
					@treTpeId INT,
					@crsTpeId INT,
					@statusRet INT,
					@leafId INT
					
			BEGIN TRANSACTION DELETE_NODE
					
			SELECT	@pgeId = cPgeId, 
					@crsTpeId = cCrsTpeId, 
					@treTpeId = cTreTpeId
			FROM tblContTree
			WHERE cId = @id

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
					
					EXECUTE spContTreRemLocationLeafs	@leafId, 
														@statusRet OUTPUT
					
					IF (@statusRet <> 0)
						BEGIN
							SELECT @status = @statusRet
							CLOSE get_leafs
							DEALLOCATE get_leafs
							ROLLBACK TRANSACTION DELETE_NODE
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
					ROLLBACK TRANSACTION DELETE_NODE
					RETURN
				END		
			END
			
			-- Examine if the node is connected to a page and not crosspublished
			IF (@pgeId IS NOT NULL) AND (@crsTpeId = 1)
			BEGIN
				EXECUTE spContPgeRemPage @pgeId, @statusRet OUTPUT
				IF (@statusRet <> 0) OR (@@ERROR <> 0)
				BEGIN
					SELECT @status = @statusRet
					ROLLBACK TRANSACTION DELETE_NODE
					RETURN
				END		
			END	
					
			DELETE FROM	tblContTreeBaseGroups
			WHERE		cTreId = @id
			
			DELETE FROM tblContTree
			WHERE		cId = @id

			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
					ROLLBACK TRANSACTION DELETE_NODE
					RETURN
				END
										
			IF (@@ERROR = 0)
				BEGIN
					COMMIT TRANSACTION DELETE_NODE
				END
		END
