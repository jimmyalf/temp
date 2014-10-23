CREATE PROCEDURE spDocumentDeleteNodeChildren
					@id INT,
					@status INT OUTPUT
AS

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
