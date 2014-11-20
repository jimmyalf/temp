
CREATE PROCEDURE spContChangeAllForTemplate 
					@id INT, 
					@status INT OUTPUT
	AS
		DECLARE	@treId INT
				
		DECLARE get_pages CURSOR FOR
			SELECT	cId
			FROM	tblContTree
			WHERE	cTemplate = @id

		UPDATE	tblContTree
		SET		cPublishedDate = NULL
		WHERE	cTemplate = @id
		
		OPEN get_pages
		FETCH NEXT FROM get_pages INTO	@treId
										
		WHILE (@@FETCH_STATUS <> -1)
			BEGIN
				EXECUTE spContChangeAllForTemplateRecursive @treId, @status
				
				FETCH NEXT FROM get_pages INTO	@treId
			END	
			
		CLOSE get_pages
		DEALLOCATE get_pages

		IF (@status = 0)
			BEGIN
				SELECT @status = @@ERROR
			END
