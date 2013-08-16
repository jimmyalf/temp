CREATE PROCEDURE spNewsAddUpdateDeleteCategory
					@type INT,
					@id INT OUTPUT,
					@languageid INT = 0,
					@name NVARCHAR(255) = '',
					@status INT OUTPUT
					
	AS
	BEGIN	
		DECLARE @stringId INT	
		DECLARE @orderId INT
		BEGIN TRANSACTION ADD_UPDATE_DELETE_CATEGORY
		IF (@type = 0) --Create
		BEGIN
			SET @stringId = 1
			SELECT TOP 1 @stringId = cId + 1
			FROM tblNewsResources
			ORDER BY cId DESC		
			INSERT INTO tblNewsResources
				(cId, cLanguageId, cResource)
			VALUES
				(@stringId, @languageid, @name)
			
			SET @orderId = 1
			SELECT TOP 1 @orderId = cOrder + 1
			FROM tblNewsCategory
			ORDER BY cOrder DESC
			INSERT INTO tblNewsCategory
				(cResourceId, cOrder)
			VALUES
				(@stringId, @orderId) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1) --Update
		BEGIN
			SELECT @stringId = cResourceId 
			FROM tblNewsCategory
			WHERE cId = @id
			
			UPDATE tblNewsResources
			SET cResource = @name
			WHERE cId = @stringId AND cLanguageId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblNewsResources
					(cId, cLanguageId, cResource)
				VALUES
					(@stringId, @languageid, @name)
			END
		END
		IF (@type = 2) --Delete
		BEGIN
			SELECT @stringId = cResourceId
			FROM tblNewsCategory
			WHERE cId = @id	
			
			DELETE FROM tblNewsResources
			WHERE cId = @stringId
			
			DELETE FROM tblNewsCategoryConnection
			WHERE cCategoryId = @id
			
			DELETE FROM tblNewsCategory
			WHERE cId = @id
		END
		
		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION ADD_UPDATE_DELETE_CATEGORY
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION ADD_UPDATE_DELETE_CATEGORY
		END
	END
