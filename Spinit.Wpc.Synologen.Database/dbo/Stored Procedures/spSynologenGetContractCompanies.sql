CREATE PROCEDURE spSynologenGetContractCompanies
					@activeType INT,
					@companyId INT,					
					@contractId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)

	SELECT @sql= 'SELECT 
					tblSynologenCompany.*,
					(SELECT cName FROM tblSynologenContract WHERE tblSynologenContract.cId = tblSynologenCompany.cContractCustomerId) AS cContractName,
					(SELECT COUNT(cCompanyValidationRuleId) FROM tblSynologenCompanyValidationRuleConnection WHERE tblSynologenCompanyValidationRuleConnection.cCompanyId = tblSynologenCompany.cId) AS cNumberOfValidationRules,
					(SELECT cName FROM tblSynologenInvoiceMethod WHERE tblSynologenInvoiceMethod.cId = tblSynologenCompany.cInvoicingMethodId) AS cInvoiceMethodName  
				 FROM tblSynologenCompany WHERE 1=1'

	IF (@activeType IS NOT NULL AND @activeType > 0) BEGIN
		IF (@activeType = 1) BEGIN
			SELECT @sql = @sql + ' AND tblSynologenCompany.cActive = 1'
		END
		IF (@activeType = 2) BEGIN
			SELECT @sql = @sql + ' AND tblSynologenCompany.cActive = 0'
		END
		-- IF (@activeType = 3) Select all
	END

	IF (@companyId IS NOT NULL AND @companyId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenCompany.cId = @xCompanyId'
	END

	IF (@contractId IS NOT NULL AND @contractId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenCompany.cContractCustomerId = @xContractId'
	END

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END

	SELECT @paramlist = '@xCompanyId INT, @xContractId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@companyId,
						@contractId
				

END
