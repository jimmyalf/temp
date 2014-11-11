CREATE PROCEDURE spNewsGetConnectedCategories
					@type INT,
					@categoryId INT,
					@locationId INT,
					@languageId INT,
					@defaultLanguageId INT,
					@newsId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@TYPE = 0)
			BEGIN
				SELECT * 
				FROM sfNewsGetAllCategory(@locationId,
				@languageId,
				@defaultLanguageId) 
				WHERE cCategoryId = @categoryId
			END
			
			IF (@type = 1)
			BEGIN
				SELECT * 
				FROM sfNewsGetAllCategory(@locationId,
				@languageId,
				@defaultLanguageId)
			END
			IF (@type = 2)
			BEGIN
				SELECT * FROM sfNewsGetCategory(@newsId, 
				@locationId,
				@languageId,
				@defaultLanguageId)
			END
			SELECT @status = @@ERROR
		END
