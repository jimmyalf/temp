CREATE PROCEDURE spSynologenAddUpdateDeleteShop
		@type INT,
		@categoryId INT,
		@shopName NVARCHAR(50) = '',
		@shopNumber NVARCHAR(50) = NULL,
		@organizationNumber NVARCHAR(50) = NULL,
		@shopDescription NVARCHAR(255) = NULL,
		@contactFirstName NVARCHAR(50) = NULL,
		@contactLastName NVARCHAR(50) = NULL,
		@email NVARCHAR(50) = NULL,
		@phone NVARCHAR(50) = NULL,
		@phone2 NVARCHAR(50) = NULL,
		@fax NVARCHAR(50) = NULL,
		@url NVARCHAR(50) = NULL,
		@mapUrl NVARCHAR(255) = NULL,
		@address NVARCHAR(50) = NULL,
		@address2 NVARCHAR(50) = NULL,
		@zip NVARCHAR(50) = NULL,
		@city NVARCHAR(50) = NULL,
		@latitude decimal(9,6) = NULL,
		@longitude decimal(9,6) = NULL,
		@active BIT = 1,
        @giroId INT = 0,
        @giroNumber NVARCHAR(50) = NULL,
        @giroSupplier NVARCHAR(100) = NULL,
        @shopAccess INT = 0,
		@externalAccessUsername NVARCHAR(50) = NULL, 
		@externalAccessHashedPassword NVARCHAR(50) = NULL,
		@shopGroupId INT = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SHOP

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenShop
				(cShopName,	cShopNumber, cOrganizationNumber, cShopDescription, 
				cContactFirstName, cContactLastName, cEmail,
				cPhone, cPhone2, cFax, cUrl, cMapUrl, cAddress, 
				cAddress2, cZip, cCity, cLatitude, cLongitude, cActive, cCategoryId,
				cGiroId, cGiroNumber, cGiroSupplier, cShopAccess, 
				cExternalAccessUsername, cExternalAccessHashedPassword, cShopGroupId)
			VALUES
				(@shopName, @shopNumber, @organizationNumber, @shopDescription, 
				@contactFirstName, @contactLastName, @email,
				@phone, @phone2, @fax, @url, @mapUrl, @address,
				@address2, @zip, @city, @latitude, @longitude, @active, @categoryId,
				@giroId, @giroNumber, @giroSupplier, @shopAccess, 
				@externalAccessUsername, @externalAccessHashedPassword, @shopGroupId)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenShop
				SET 
				cShopName = @shopName,
				cShopNumber = @shopNumber,
				cOrganizationNumber = @organizationNumber,
				cShopDescription = @shopDescription,
				cContactFirstName = @contactFirstName,
				cContactLastName = @contactLastName,			
				cEmail = @email,
				cPhone = @phone,
				cPhone2 = @phone2,
				cFax = @fax,
				cUrl = @url,
				cMapUrl = @mapUrl,
				cAddress = @address,
				cAddress2 = @address2,
				cZip = @zip,
				cCity = @city,
				cActive = @active,
				cCategoryId = @categoryId,
				cGiroId = @giroId, 
				cGiroNumber = @giroNumber, 
				cGiroSupplier = @giroSupplier,
				cShopAccess = @shopAccess,
				cLatitude = @latitude,
				cLongitude = @longitude,
				cExternalAccessUsername = @externalAccessUsername, 
				cExternalAccessHashedPassword = @externalAccessHashedPassword,
				cShopGroupId = @shopGroupId
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN

			DELETE FROM tblSynologenShopMemberConnection
			WHERE cSynologenShopId = @id		

			DELETE FROM tblSynologenShopContractConnection			
			WHERE cSynologenShopId = @id

			DELETE FROM tblSynologenShop
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SHOP
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SHOP
			END
