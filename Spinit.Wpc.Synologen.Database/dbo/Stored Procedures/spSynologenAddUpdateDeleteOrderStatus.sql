CREATE PROCEDURE spSynologenAddUpdateDeleteOrderStatus
		@type INT,
		@name NVARCHAR(50),
		@orderNumber INT,
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_OS

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenOrderStatus (cName, cOrder)
			VALUES (@name, @orderNumber)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenOrderStatus
				SET cName = @name,
				cOrder = @orderNumber
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN
			DELETE FROM tblSynologenOrderStatus			
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_OS
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_OS
			END
