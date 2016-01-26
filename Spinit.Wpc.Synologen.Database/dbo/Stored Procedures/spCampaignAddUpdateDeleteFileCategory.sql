CREATE PROCEDURE spCampaignAddUpdateDeleteFileCategory
					@type INT,
					@name NVARCHAR(255) = '',
					@mustOrder BIT = 0,
					@status INT OUTPUT,
					@id INT OUTPUT
					
	AS
	BEGIN	
		DECLARE @stringId INT	
		BEGIN TRANSACTION ADD_UPDATE_DELETE_FILECATEGORY
		IF (@type = 0)
		BEGIN
			INSERT INTO tblCampaignFileCategory
				(cDescription,cMustOrder)
			VALUES
				(@name,@mustOrder) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1)
		BEGIN
			
			UPDATE tblCampaignFileCategory
			SET cDescription = @name,
			cMustOrder = @mustOrder
			WHERE cId = @id
			
		END
		IF (@type = 2)
		BEGIN			
			DELETE FROM tblCampaignFileCategory
			WHERE cId = @id
		END
		
		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION ADD_UPDATE_DELETE_FILECATEGORY
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION ADD_UPDATE_DELETE_FILECATEGORY
		END
	END
