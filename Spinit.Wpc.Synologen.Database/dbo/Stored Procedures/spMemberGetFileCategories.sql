CREATE PROCEDURE spMemberGetFileCategories
					@type INT,
					@categoryid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	tblMemberFileCategory
					WHERE cId = @categoryId								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	tblMemberFileCategory
					ORDER BY cDescription ASC	
				END
			SELECT @status = @@ERROR
		END
