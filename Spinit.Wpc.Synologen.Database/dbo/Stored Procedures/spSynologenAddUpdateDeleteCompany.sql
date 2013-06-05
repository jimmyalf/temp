CREATE PROCEDURE spSynologenAddUpdateDeleteCompany
		@type INT,
		@contractId INT = 0,
		@name NVARCHAR(50) = NULL,
		@address1 NVARCHAR(50) = NULL,
		@address2 NVARCHAR(50) = NULL,
		@zip NVARCHAR(50) = NULL,
		@city NVARCHAR(50) = NULL,
		@companyCode NVARCHAR(16) = NULL,
		@bankCode NVARCHAR(50) = NULL,
		@active BIT = 1,
		@organizationNumber NVARCHAR(50) = NULL,
		@invoiceCompanyName NVARCHAR(255) = NULL,
		@taxAccountingCode NVARCHAR(50) = NULL,
		@paymentDuePeriod INT = 0,
		@ediRecipientId NVARCHAR(50) = NULL,
		@invoicingMethodId INT = 0,
		@invoiceFreeText NVARCHAR(2000) = NULL,
		@countryId INT = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION ContractCompanyTransaction

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenCompany
				(cName,	cAddress1,	cAddress2, cZip, cCity, cContractCustomerId,
				cCompanyCode, cBankCode, cActive,
				cOrganizationNumber, cInvoiceCompanyName, cPaymentDuePeriod,
				cTaxAccountingCode, cEDIRecipientId, cInvoicingMethodId, cInvoiceFreeText, 
				cCountryId)
			VALUES
				(@name,	@address1,	@address2, @zip, @city, @contractId,
				@companyCode, @bankCode, @active,
				@organizationNumber, @invoiceCompanyName, @paymentDuePeriod,
				@taxAccountingCode, @ediRecipientId, @invoicingMethodId, @invoiceFreeText,  
				@countryId)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenCompany
				SET 
				cName = @name,
				cAddress1 = @address1,
				cAddress2 = @address2,
				cZip = @zip,
				cCity = @city,			
				cContractCustomerId = @contractId,
				cCompanyCode = @companyCode,
				cBankCode = @bankCode,
				cActive = @active,
				cOrganizationNumber = @organizationNumber,
				cInvoiceCompanyName = @invoiceCompanyName,
				cPaymentDuePeriod = @paymentDuePeriod,
				cTaxAccountingCode = @taxAccountingCode,
				cEDIRecipientId = @ediRecipientId,
				cInvoicingMethodId = @invoicingMethodId,
				cInvoiceFreeText = @invoiceFreeText,
				cCountryId = @countryId
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN --Delete
			--Delete connections
			DELETE FROM tblSynologenCompanyValidationRuleConnection
			WHERE cCompanyId = @id

			DELETE FROM tblSynologenCompany
			WHERE cId = @id					
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ContractCompanyTransaction
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ContractCompanyTransaction
			END
