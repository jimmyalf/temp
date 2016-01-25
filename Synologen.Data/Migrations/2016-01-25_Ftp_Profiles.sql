begin transaction
go

create table tblSynologenContractFtpProfile(
  cId           int           not null identity constraint PK_tblSynologenContractFtpProfile primary key,
  cName         nvarchar(50)  not null,
  cServerUrl    nvarchar(255) not null,
  cProtocolType int           not null,
  cUsername     nvarchar(255) not null,
  cPassword     nvarchar(255) not null,
  cPassiveFtp   bit           not null
)
go

alter table tblSynologenCompany add
  cCustomFtpProfileId int NULL
go

set ANSI_NULLS on
go

alter PROCEDURE spSynologenAddUpdateDeleteCompany
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
		@ediRecipientQuantifier NVARCHAR(50) = NULL,
		@invoicingMethodId INT = 0,
		@ftpProfileId INT = NULL,
		@invoiceFreeText NVARCHAR(2000) = NULL,
		@countryId INT = NULL,
		@derivedfromcompanyid INT,
		@email NVARCHAR(100),
		@status INT OUTPUT,
		@id INT OUTPUT

	AS BEGIN TRANSACTION ContractCompanyTransaction

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenCompany
				(cName,	cAddress1,	cAddress2, cZip, cCity, cContractCustomerId,
				cCompanyCode, cBankCode, cActive,
				cOrganizationNumber, cInvoiceCompanyName, cPaymentDuePeriod,
				cTaxAccountingCode, cEDIRecipientId, cEDIRecipientQuantifier, 
				cInvoicingMethodId, cInvoiceFreeText, cCountryId, cDerivedFromCompanyId, 
				cEmail, cCustomFtpProfileId)
			VALUES
				(@name,	@address1,	@address2, @zip, @city, @contractId,
				@companyCode, @bankCode, @active,
				@organizationNumber, @invoiceCompanyName, @paymentDuePeriod,
				@taxAccountingCode, @ediRecipientId, @ediRecipientQuantifier, 
				@invoicingMethodId, @invoiceFreeText, @countryId, @derivedfromcompanyid, 
				@email, @ftpProfileId)
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
				cEDIRecipientQuantifier = @ediRecipientQuantifier,
				cInvoicingMethodId = @invoicingMethodId,
				cInvoiceFreeText = @invoiceFreeText,
				cCountryId = @countryId,
				cEmail = @email,
				cCustomFtpProfileId = @ftpProfileId
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


/****** Object:  StoredProcedure [dbo].[spSynologenGetContractCompanies]    Script Date: 2014-11-11 15:13:55 ******/
SET ANSI_NULLS ON
go

alter table tblSynologenCompany add
  constraint FK_tblSynologenCompany_tblSynologenContractFtpProfile foreign key(cCustomFtpProfileId) references tblSynologenContractFtpProfile(cId)
go

commit