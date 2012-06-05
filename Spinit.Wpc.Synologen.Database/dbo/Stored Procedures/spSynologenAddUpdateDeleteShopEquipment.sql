CREATE PROCEDURE spSynologenAddUpdateDeleteShopEquipment
		@type INT,
		@name NVARCHAR(50) = NULL,
		@description NVARCHAR(500) = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SE

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenShopEquipment (cName,cDescription)
			VALUES (@name,@description)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenShopEquipment
				SET cName = @name,
				cDescription = @description
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN
			DELETE FROM tblSynologenShopEquipment
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SE
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SE
			END
