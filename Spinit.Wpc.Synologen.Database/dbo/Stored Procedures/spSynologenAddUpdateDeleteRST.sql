CREATE PROCEDURE spSynologenAddUpdateDeleteRST
		@type INT,
		@companyId INT = 0,
		@name NVARCHAR(50) = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION RSTTransaction

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenRst
				(cCompanyId, cName)
			VALUES
				(@companyId,@name)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenRst
				SET 
				cName = @name,	
				cCompanyId = @companyId
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN --Delete
			--Delete connections
			DELETE FROM tblSynologenRst
			WHERE cId = @id					
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION RSTTransaction
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION RSTTransaction
			END
