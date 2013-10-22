CREATE PROCEDURE spSynologenGetOrders
					@orderId INT,
					@shopId INT,
					@contractId INT,
					@salesPersonMemberId INT,
					@companyId INT,
					@invoiceNumberId BIGINT,
					@statusId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenOrder.*,
			tblSynologenOrder.cCustomerFirstName + '' '' + tblSynologenOrder.cCustomerLastName AS cCustomerName,
			(SELECT COUNT(*) FROM tblSynologenOrderItems WHERE tblSynologenOrderItems.cOrderId = tblSynologenOrder.cId) AS cOrderItems,
			tblSynologenCompany.cName AS cCompanyName,
			(SELECT cName FROM tblSynologenContract WHERE tblSynologenContract.cId = tblSynologenCompany.cContractCustomerId) AS cContractName,
			(SELECT cName FROM tblSynologenOrderStatus WHERE tblSynologenOrderStatus.cId = tblSynologenOrder.cStatusId) AS cStatusName
			FROM tblSynologenOrder
			INNER JOIN tblSynologenCompany ON tblSynologenCompany.cId = tblSynologenOrder.cCompanyId'


		
	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@contractId IS NOT NULL AND @contractId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenCompany.cContractCustomerId = @xContractId'
	END
	IF (@orderId IS NOT NULL AND @orderId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cId = @xOrderId'
	END
	IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cSalesPersonShopId = @xShopId'
	END
	IF (@salesPersonMemberId IS NOT NULL AND @salesPersonMemberId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cSalesPersonMemberId = @xMemberId'
	END
	IF (@companyId IS NOT NULL AND @companyId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cCompanyId = @xCompanyId'
	END
	IF (@invoiceNumberId IS NOT NULL AND @invoiceNumberId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cInvoiceNumber = @xInvoiceNumberId'
	END
	IF (@statusId IS NOT NULL AND @statusId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cStatusId = @xStatusId'
	END
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xContractId INT, @xOrderId INT, @xShopId INT, @xMemberId INT, @xCompanyId INT, @xStatusId INT, @xInvoiceNumberId BIGINT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@contractId,
						@orderId,
						@shopId,
						@salesPersonMemberId,
						@companyId,
						@statusId,
						@invoiceNumberId
				

END
