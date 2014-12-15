/*
 * spDocumentMove
 * @status > 0 => SqlError
 * @status = 0 => Ok
 * @status = -1 => Document does not extist.
 */



create PROCEDURE spDocumentMove
					@direction INT,
					@id INT,
					@status INT OUTPUT
	AS
	
		DECLARE @order INT,
			@max INT,
			@nodeId INT
							
						
		DECLARE get_doc CURSOR LOCAL FOR
			SELECT	cNodeId, cOrder
			FROM	tblDocuments
			WHERE	cId = @id
			
		OPEN get_doc
		FETCH NEXT FROM get_doc INTO @nodeId, @order
										
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_doc
				DEALLOCATE get_doc
				
				SET @status = -1
				RETURN
			END
			
		CLOSE get_doc
		DEALLOCATE get_doc
		
		/*Get max*/
		SELECT	@max = MAX (cOrder)
		FROM	tblDocuments
		WHERE cNodeId = @nodeId
		
		
		IF (((@order = 1) AND (@direction = 0))
			OR ((@order = @max) AND (@direction = 1)))
			BEGIN
				SET @status = 0
				RETURN
			END
	
	
	IF @direction = 0 /*Up*/
	
	BEGIN
		BEGIN TRANSACTION MOVE_DOCUMENT_UP
		UPDATE	tblDocuments
		SET		cOrder = -1
		WHERE	cNodeId = @nodeId
		AND		cId = @id
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_UP
				RETURN
			END
		
		UPDATE	tblDocuments
		SET		cOrder = @order
		WHERE	cNodeId = @nodeId
		AND		cOrder = @order - 1
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_UP
				RETURN
			END
		
		UPDATE	tblDocuments
		SET		cOrder = @order - 1
		WHERE	cNodeId = @nodeId
		AND		cId = @id
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_UP
				RETURN
			END
		
		COMMIT TRANSACTION MOVE_DOCUMENT_UP
	END
	
	IF @direction = 1 /*Down*/
	BEGIN
		BEGIN TRANSACTION MOVE_DOCUMENT_DOWN
		
		UPDATE	tblDocuments
		SET		cOrder = -1
		WHERE	cNodeId = @nodeId
		AND		cId = @id
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_DOWN
				RETURN
			END
		
		UPDATE	tblDocuments
		SET		cOrder = @order
		WHERE	cNodeId = @nodeId
		AND		cOrder = @order + 1
			
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_DOWN
				RETURN
			END
				
		UPDATE	tblDocuments
		SET		cOrder = @order + 1
		WHERE	cNodeId = @nodeId
		AND		cId = @id
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_DOCUMENT_DOWN
				RETURN
			END
			
		COMMIT TRANSACTION MOVE_DOCUMENT_DOWN
		
		
	END
	
	IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
