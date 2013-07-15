CREATE PROCEDURE spMemberGetCategories
					@type INT,
					@categoryid INT,
					@locationid INT,
					@languageid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	sfMemberGetAllMembersCategories(@locationid,
					@languageid)
					WHERE cCategoryId = @categoryId								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	sfMemberGetAllMembersCategories(@locationid,
					@languageid)
					ORDER BY cName ASC	
				END
			SELECT @status = @@ERROR
		END
