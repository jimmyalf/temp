CREATE PROCEDURE spContTreLockPage
					@id INT,
					@lock BIT,
					@recursive BIT,
					@lockedBy NVARCHAR (100),
					@status INT OUTPUT
	AS
		DECLARE	@pgeLockedBy NVARCHAR (100)
		
		DECLARE chk_locked CURSOR FOR
			SELECT	cLockedBy
			FROM	tblContTree
			WHERE	cId = @id
			
		OPEN chk_locked
		FETCH NEXT FROM chk_locked INTO @pgeLockedBy
		
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE chk_locked
				DEALLOCATE chk_locked
				
				SELECT @status = -3
				RETURN
			END
			
		CLOSE chk_locked
		DEALLOCATE chk_locked
		
		SELECT @status = 0
		
		IF (@recursive = 1)
			BEGIN
				DECLARE	@leafId INT,
						@retStatus INT
						
				DECLARE get_leafs CURSOR LOCAL FOR
					SELECT	cId
					FROM	tblContTree
					WHERE	cParent = @id
					
				OPEN get_leafs
				FETCH NEXT FROM get_leafs INTO @leafId
				
				WHILE (@@FETCH_STATUS <> -1)
					BEGIN
						EXECUTE spContTreLockPage	@leafId, 
													@lock, 
													@recursive, 
													@lockedBy, 
													@retStatus
													
						IF (@retStatus <> 0)
							BEGIN
								SELECT @status = @retStatus
							END
						
						FETCH NEXT FROM get_leafs INTO @leafId
					END
					
				CLOSE get_leafs
				DEALLOCATE get_leafs
			END
		
		IF (@lock = 1)
			BEGIN
				IF (@pgeLockedBy IS NOT NULL)
					BEGIN
						SELECT @status = 0
						RETURN
					END
					
				UPDATE	tblContTree
				SET		cLockedBy = @lockedBy,
						cLockedDate = GETDATE ()
						--,cApprovedBy = NULL
				WHERE	cId = @id
			END
		ELSE
			BEGIN
				/* --Removed because superadmin
				   --can from now on always check in
				IF (@pgeLockedBy <> @lockedBy)
					BEGIN
						SELECT @status = 0
						RETURN
					END
				*/					
				UPDATE	tblContTree
				SET		cLockedBy = NULL,
						cLockedDate = NULL
				WHERE	cId = @id
			END
		
		IF (@status = 0)
			BEGIN		
				SELECT @status = @@ERROR
			END
