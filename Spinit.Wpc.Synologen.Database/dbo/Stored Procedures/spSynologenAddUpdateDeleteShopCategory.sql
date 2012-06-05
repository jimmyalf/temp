CREATE PROCEDURE spSynologenAddUpdateDeleteShopCategory
		@type INT,
		@name NVARCHAR(50) = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SC

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenShopCategory (cName)
			VALUES (@name)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenShopCategory
				SET cName = @name
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN
			DELETE FROM tblSynologenShopCategory
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SC
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SC
			END
