CREATE PROCEDURE spSynologenGetForInvoicing
					@statusId INT,
					@invoicingMethodIdFilter INT = NULL,
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
			INNER JOIN tblSynologenCompany ON tblSynologenCompany.cId = tblSynologenOrder.cCompanyId 
			WHERE tblSynologenOrder.cInvoiceNumber IS NULL'
	IF (@statusId IS NOT NULL AND @statusId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cStatusId = @xStatusId'
	END
	IF (@invoicingMethodIdFilter IS NOT NULL AND @invoicingMethodIdFilter > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenCompany.cInvoicingMethodId = @xInvoicingMethodIdFilter'
	END
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xStatusId INT, @xInvoicingMethodIdFilter INT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@statusId,
						@invoicingMethodIdFilter
				

END
