
CREATE PROCEDURE spContChangeAllForTemplateRecursive
					@id INT,
					@status INT OUTPUT
	AS
		DECLARE @treId INT
		
		BEGIN
			DECLARE get_pages CURSOR LOCAL FOR
				SELECT	cId
				FROM	tblContTree
				WHERE	cParent = @id
				
			UPDATE	tblContTree
			SET		cPublishedDate = NULL
			WHERE	cParent = @id
				AND	cTemplate IS NULL
			
			OPEN get_pages
			FETCH NEXT FROM get_pages INTO @treId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
					EXECUTE spContChangeAllForTemplateRecursive @treId, @status
				
					FETCH NEXT FROM get_pages INTO @treId
				END
		
			CLOSE get_pages
			DEALLOCATE get_pages
		END
		
		IF (@status = 0)
			BEGIN
				SELECT @status = @@ERROR
			END
