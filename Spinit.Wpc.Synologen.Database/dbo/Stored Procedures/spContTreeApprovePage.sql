CREATE PROCEDURE spContTreeApprovePage
					@id INT,
					@recursive BIT,
					@approvedBy NVARCHAR (100),
					@status INT OUTPUT
	AS
		DECLARE	@treApprovedBy NVARCHAR (100)
		
		DECLARE get_approved CURSOR FOR
			SELECT	cApprovedBy
			FROM	tblContTree
			WHERE	cId = @id
			
		OPEN get_approved
		FETCH NEXT FROM get_approved INTO @treApprovedBy
		
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_approved
				DEALLOCATE get_approved
				
				SELECT @status = -3
				RETURN
			END
			
		CLOSE get_approved
		DEALLOCATE get_approved
			
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
						EXECUTE spContTreeApprovePage	@leafId,
														@recursive,
														@approvedBy,
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
				
		IF (@treApprovedBy IS NULL)
			BEGIN
				UPDATE	tblContTree
				SET		cApprovedBy = @approvedBy,
						cApprovedDate = GETDATE ()
				WHERE	cId = @id
					AND	cLockedBy IS NULL
			END
		
		IF (@status = 0)
			BEGIN
				SELECT @status = @@ERROR
			END
