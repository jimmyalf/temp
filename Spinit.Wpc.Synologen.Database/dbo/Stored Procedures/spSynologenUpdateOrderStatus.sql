create PROCEDURE spSynologenUpdateOrderStatus
					@newStatusId INT,
					@orderId INT,
					@shopId INT,
					@contractId INT,
					@salesPersonMemberId INT,
					@companyId INT,
					@invoiceNumberId BIGINT,
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'UPDATE tblSynologenOrder
			SET cStatusId = @xNewStatusId
			FROM tblSynologenOrder'

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
	SELECT @paramlist = ' @xNewStatusId INT, @xContractId INT, @xOrderId INT, @xShopId INT, @xMemberId INT, @xCompanyId INT, @xInvoiceNumberId BIGINT'
	EXEC sp_executesql @sql,
						@paramlist, 
						@newStatusId,
						@contractId,
						@orderId,
						@shopId,
						@salesPersonMemberId,
						@companyId,
						@invoiceNumberId
				

END
