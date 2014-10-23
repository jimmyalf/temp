CREATE PROCEDURE spMemberAddUpdateDeleteCategory
					@type INT,
					@languageid INT = 0,
					@name NVARCHAR(255) = '',
					@groupid INT = 0,
					@status INT OUTPUT,
					@id INT OUTPUT
					
	AS
	BEGIN	
		DECLARE @stringId INT	
		BEGIN TRANSACTION ADD_UPDATE_DELETE_CATEGORY
		IF (@type = 0)
		BEGIN
			SET @stringId = 1
			SELECT TOP 1 @stringId = cId + 1
			FROM tblMemberLanguageStrings
			ORDER BY cId DESC		
			INSERT INTO tblMemberLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@stringId, @languageid, @name)
					
			INSERT INTO tblMemberCategories
				(cNameStringId, cBaseGroupId)
			VALUES
				(@stringId,@groupid) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1)
		BEGIN
			SELECT @stringId = cNameStringId 
			FROM tblMemberCategories
			WHERE cId = @id
			
			UPDATE tblMemberLanguageStrings
			SET cString = @name
			WHERE cId = @stringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblMemberLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@stringId, @languageid, @name)
			END
			
			UPDATE tblMemberCategories
			SET cBaseGroupId = @groupid
			WHERE cId = @id
			
		END
		IF (@type = 2)
		BEGIN
			SELECT @stringId = cNameStringId 
			FROM tblMemberCategories
			WHERE cId = @id	
			
			DELETE FROM tblMemberLanguageStrings
			WHERE cId = @stringId
			
			DELETE FROM tblMemberCategoryConnection
			WHERE cCategoryId = @id
			
			DELETE FROM tblMemberCategories
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
