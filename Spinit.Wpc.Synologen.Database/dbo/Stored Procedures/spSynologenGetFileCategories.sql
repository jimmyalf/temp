CREATE PROCEDURE spSynologenGetFileCategories
					@type INT,
					@categoryid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 1)
			BEGIN
					SELECT	*
					FROM	tblSynologenFileCategory
					WHERE cId = @categoryId								
			END
			IF (@type = 2)
				BEGIN				
					SELECT	*
					FROM	tblSynologenFileCategory
					ORDER BY cDescription ASC	
				END
			SELECT @status = @@ERROR
		END
