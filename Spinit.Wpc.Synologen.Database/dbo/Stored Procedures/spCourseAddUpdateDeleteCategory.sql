CREATE PROCEDURE spCourseAddUpdateDeleteCategory	
					@action INT,
					@id INT OUTPUT,
					@languageId INT =-1,
					@name varchar(255)= '',
					@status INT OUTPUT
AS
		DECLARE @stringId INT	
		DECLARE @orderId INT
		BEGIN TRANSACTION CATEGORY_ADD_UPDATE_DELETE
		IF (@action = 0) -- Create
			BEGIN
				SET @stringId = 1
				SELECT TOP 1 @stringId = cId + 1
				FROM tblCourseResource
				ORDER BY cId DESC		
				INSERT INTO tblCourseResource
					(cId, cLanguageId, cResource)
				VALUES
					(@stringId, @languageId, @name)
				
				SET @orderId = 1
				SELECT TOP 1 @orderId = cOrder + 1
				FROM tblCourseCategory
				ORDER BY cOrder DESC
				INSERT INTO tblCourseCategory
					(cResourceId, cOrder)
				VALUES
					(@stringId, @orderId) 
				SELECT @id = @@IDENTITY
			END		
		IF (@action = 1) -- Update
			BEGIN	
				SELECT @stringId = cResourceId 
				FROM tblCourseCategory
				WHERE cId = @id
				
				UPDATE tblCourseResource
				SET cResource = @name
				WHERE cId = @stringId AND cLanguageId = @languageId
				
				IF (@@ROWCOUNT = 0)
				BEGIN
					INSERT INTO tblCourseResource
						(cId, cLanguageId, cResource)
					VALUES
						(@stringId, @languageId, @name)
				END
			END	
		IF (@action = 2) -- Delete
			BEGIN
				SELECT @stringId = cResourceId
				FROM tblCourseCategory
				WHERE cId = @id	
				
				DELETE FROM tblCourseResource
				WHERE cId = @stringId
				
				DELETE FROM tblCourseMainCategoryConnection
				WHERE cCategoryId = @id
				
				DELETE FROM tblCourseCategory
				WHERE cId = @id
			END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION CATEGORY_ADD_UPDATE_DELETE
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION CATEGORY_ADD_UPDATE_DELETE
			END
