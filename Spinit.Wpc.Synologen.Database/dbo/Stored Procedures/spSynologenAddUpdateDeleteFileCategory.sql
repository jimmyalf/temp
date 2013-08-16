﻿CREATE PROCEDURE spSynologenAddUpdateDeleteFileCategory
					@type INT,
					@name NVARCHAR(255) = '',
					@status INT OUTPUT,
					@id INT OUTPUT
					
	AS
	BEGIN	
		DECLARE @stringId INT	
		BEGIN TRANSACTION ADD_UPDATE_DELETE_FILECATEGORY
		IF (@type = 0)
		BEGIN
			INSERT INTO tblSynologenFileCategory(cDescription)
			VALUES(@name) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1)
		BEGIN
			UPDATE tblSynologenFileCategory
			SET cDescription = @name
			WHERE cId = @id
			
		END
		IF (@type = 2)
		BEGIN			
			DELETE FROM tblSynologenFileCategory
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
