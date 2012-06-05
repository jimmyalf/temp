CREATE PROCEDURE spSynologenAddUpdateDeleteContract
		@type INT,
		@code NVARCHAR(50) = '',
		@name NVARCHAR(50) = NULL,
		@description NVARCHAR(500) = NULL,
		@address NVARCHAR(50) = NULL,
		@address2 NVARCHAR(50) = NULL,
		@zip NVARCHAR(50) = NULL,
		@city NVARCHAR(50) = NULL,
		@phone NVARCHAR(50) = NULL,
		@phone2 NVARCHAR(50) = NULL,
		@fax NVARCHAR(50) = NULL,
		@email NVARCHAR(50) = NULL,
		@active BIT = 1,
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_CCUSTOMER

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenContract
				(cCode,	cName, cDescription, cAddress, 
				cAddress2, cZip, cCity, cPhone, cPhone2, 
				cFax, cEmail, cActive)
			VALUES
				(@code, @name, @description, @address, 
				@address2, @zip, @city, @phone, @phone2,
				@fax, @email, @active)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenContract
				SET 
				cCode = @code,
				cName = @name,
				cDescription = @description,
				cAddress = @address,
				cAddress2 = @address2,			
				cZip = @zip,
				cCity = @city,
				cPhone = @phone,
				cPhone2 = @phone2,
				cFax = @fax,
				cEmail = @email,
				cActive = @active
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN --Delete
			--Delete connections
			DELETE FROM tblSynologenShopContractConnection
			WHERE cSynologenContractCustomerId = @id

			DELETE FROM tblSynologenContract
			WHERE cId = @id					
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_CCUSTOMER
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_CCUSTOMER
			END
