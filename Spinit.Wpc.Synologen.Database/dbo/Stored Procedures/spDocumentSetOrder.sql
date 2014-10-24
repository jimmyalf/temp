create PROCEDURE spDocumentSetOrder
AS
	DECLARE @order INT
		DECLARE @nodeId	INT	
		DECLARE @documentId	INT	
		
		
		
		DECLARE get_node CURSOR LOCAL FOR
			SELECT	cId
			FROM	tblDocumentNode
			
			
		OPEN get_node
		FETCH NEXT FROM get_node INTO @nodeId
										
		WHILE (@@FETCH_STATUS <> -1)
			BEGIN			
				SET @order = 1
				DECLARE get_document CURSOR LOCAL FOR
				SELECT	cId
				FROM	tblDocuments
				WHERE cNodeId = @nodeId
				ORDER BY cName
				
				
				OPEN get_document
				FETCH NEXT FROM get_document INTO @documentId
				WHILE (@@FETCH_STATUS <> -1)
				BEGIN
				
					UPDATE tblDocuments
					SET cOrder=@order
					WHERE cId = @documentId
					
					SET @order = @order + 1
					PRINT @order
					FETCH NEXT FROM get_document INTO @documentId
				END
				CLOSE get_document
				DEALLOCATE get_document
						
				FETCH NEXT FROM get_node INTO @nodeId
			END
			
		CLOSE get_node
		DEALLOCATE get_node
