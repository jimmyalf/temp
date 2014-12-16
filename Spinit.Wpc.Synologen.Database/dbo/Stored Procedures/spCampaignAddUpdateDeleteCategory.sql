CREATE PROCEDURE spCampaignAddUpdateDeleteCategory
					@type INT,
					@languageid INT = 0,
					@name NVARCHAR(255) = '',
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
			FROM tblCampaignLanguageStrings
			ORDER BY cId DESC		
			INSERT INTO tblCampaignLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@stringId, @languageid, @name)
					
			INSERT INTO tblCampaignCategories
				(cNameStringId)
			VALUES
				(@stringId) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1)
		BEGIN
			SELECT @stringId = cNameStringId 
			FROM tblCampaignCategories
			WHERE cId = @id
			
			UPDATE tblCampaignLanguageStrings
			SET cString = @name
			WHERE cId = @stringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblCampaignLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@stringId, @languageid, @name)
			END
		END
		IF (@type = 2)
		BEGIN
			SELECT @stringId = cNameStringId 
			FROM tblCampaignCategories
			WHERE cId = @id	
			
			DELETE FROM tblCampaignLanguageStrings
			WHERE cId = @stringId
			
			DELETE FROM tblCampaignCategoryConnection
			WHERE cCategoryId = @id
			
			DELETE FROM tblCampaignCategories
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
