CREATE PROCEDURE spDocumentAddUpdateDeleteNode
					@action INT,
					@id INT OUTPUT,
					@parentId INT = 0,
					@name NVARCHAR(255) = '',
					@groupId INT = -1,
					@userId INT = -1,
					@sortType INT = -1,
					@status INT OUTPUT	
AS
		BEGIN TRANSACTION ADD_UPDATE_DELETE_NODE
		IF (@action = 0) -- Create
		BEGIN
			INSERT INTO tblDocumentNode
				(cParentId, cName, cGroupId, cUserId, cSortType)				
			VALUES
				(@parentId, @name, @groupId, @userId, @sortType)				
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN
			UPDATE tblDocumentNode
			SET cParentId = @parentId, 
				cName = @name,
				cGroupId = @groupId,
				cUserId = @userId,
				cSortType = @sortType
			WHERE cId = @id

			--IF SortType = order update documents with a default order
			IF @sortType = 1
			BEGIN
				DECLARE @order INT 
				
				SELECT	@order = MAX (cOrder)
				FROM	tblDocuments
				WHERE cNodeId = @id
				AND cOrder IS NOT NULL
				IF @order IS NULL
				BEGIN
					SET @order = 0
				END
				
				UPDATE tblDocuments
				SET cOrder = @order,
				@order = @order +1
				WHERE cNodeId = @id
				AND cOrder IS NULL

			END
		END
		IF (@action = 2) -- Delete
		BEGIN
			
			DECLARE @leafId INT,
			@statusRet INT

			-- Examine if there are any childs to remove 
			DECLARE get_leafs CURSOR LOCAL FOR 
				SELECT	cId 
				FROM	tblDocumentNode
				WHERE	cParentId = @id
				
			SELECT @status = 0
							
			OPEN get_leafs
			FETCH NEXT FROM get_leafs INTO @leafId
					
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN

					-- Recursive call					
					EXECUTE spDocumentDeleteNodeChildren @leafId, @statusRet OUTPUT
					
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

			-- Delete connections

			DELETE FROM tblDocumentNodeLocationConnection
			WHERE cDocumentNodeId = @id
			
			DELETE FROM tblDocumentNodeLanguageConnection
			WHERE cDocumentNodeId = @id
			
			DELETE FROM dbo.tblDocumentNodeFileConnection
			WHERE cDocumentNodeId = @id
			
			-- Delete documents 
			
			DELETE FROM tblDocumentDocumentFileConnection
			WHERE cDocumentId IN (SELECT cId FROM dbo.tblDocuments WHERE cNodeId = @id)
			
			DELETE FROM tblDocuments
			WHERE cNodeId = @id

			-- Delete node

			DELETE FROM tblDocumentNode
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_NODE
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_NODE
			END
