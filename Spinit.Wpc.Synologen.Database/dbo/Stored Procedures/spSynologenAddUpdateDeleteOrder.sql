CREATE PROCEDURE spSynologenAddUpdateDeleteOrder
		@type INT,
		@companyId INT = 0,
		@rstText NVARCHAR(50) = NULL,
		@statusId INT = 0,
		@salesPersonMemberId INT = 0,
		@salesPersonShopId INT = 0,
		@companyUnit NVARCHAR(255) = NULL,
		@customerFirstName NVARCHAR(50) = NULL,
		@customerLastName NVARCHAR(255) = NULL,
		@personalIdNumber NVARCHAR(50) = NULL,
		@email NVARCHAR(50) = NULL,
		@phone NVARCHAR(50) = NULL,
		@orderPayedToShop BIT = 0,
		@invoiceNumber BIGINT = NULL,
		@invoiceSumIncludingVAT FLOAT = NULL,
		@invoiceSumExcludingVAT FLOAT = NULL,		
		@customerOrderNumber NVARCHAR(50) = NULL,		
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_ORDER

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenOrder
				(cCompanyId, cRstText, cSalesPersonShopId, cSalesPersonMemberId,
				cStatusId, cCompanyUnit, cCustomerFirstName, cCustomerLastName, 
				cPersonalIdNumber, cPhone, cEmail, cCreatedDate,cOrderMarkedAsPayed,
				cInvoiceNumber, cCustomerOrderNumber)
			VALUES
				(@companyId, @rstText, @salesPersonShopId, @salesPersonMemberId,
				@statusId, @companyUnit, @customerFirstName, @customerLastName, 
				@personalIdNumber, @phone, @email, GETDATE(), 0,
				@invoiceNumber,@customerOrderNumber)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenOrder
				SET 
				cCompanyId = @companyId,
				cRstText = @rstText,
				cSalesPersonShopId = @salesPersonShopId,
				cSalesPersonMemberId = @salesPersonMemberId,
				cStatusId = @statusId,			
				cCompanyUnit = @companyUnit,
				cCustomerFirstName = @customerFirstName,
				cCustomerLastName = @customerLastName,
				cPersonalIdNumber = @personalIdNumber,
				cPhone = @phone,
				cEmail = @email,
				cUpdatedDate = GETDATE(),
				cOrderMarkedAsPayed = @orderPayedToShop,
				cInvoiceNumber = @invoiceNumber,
				cInvoiceSumIncludingVAT = @invoiceSumIncludingVAT,
				cInvoiceSumExcludingVAT = @invoiceSumExcludingVAT,
				cCustomerOrderNumber = @customerOrderNumber
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN
			DELETE FROM tblSynologenOrderItems			
			WHERE cOrderId = @id

			DELETE FROM tblSynologenOrder
			WHERE cId = @id	
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_ORDER
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_ORDER
			END
