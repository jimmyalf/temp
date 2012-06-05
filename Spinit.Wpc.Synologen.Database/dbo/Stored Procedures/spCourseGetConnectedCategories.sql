CREATE PROCEDURE spCourseGetConnectedCategories
@type INT, @categoryId INT, @locationId INT, @languageId INT, @defaultLanguageId INT, @MainId INT, @status INT OUTPUT
AS
IF (@type = 0)
			BEGIN			
				SELECT * FROM [sfCourseGetAllCategory](
					@locationId,
					@languageId,
					@defaultLanguageId) 
				WHERE cCategoryId = @categoryId
			END
		IF (@type = 1)
			BEGIN
				SELECT * FROM [sfCourseGetAllCategory](
					@locationId,
					@languageId,
					@defaultLanguageId)		
			END		
		IF (@type = 7)
			BEGIN
				SELECT * FROM [sfCourseGetCategory](
					@mainId, 
					@locationId,
					@languageId,
					@defaultLanguageId)		
			END		
	
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
