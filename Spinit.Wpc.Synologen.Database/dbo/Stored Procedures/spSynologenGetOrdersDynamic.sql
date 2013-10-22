CREATE PROCEDURE [dbo].[spSynologenGetOrdersDynamic]
					@contractId INT,
					@statusId INT,
					@settlementId INT,
					@createDateStartLimit SMALLDATETIME,
					@createDateStopLimit SMALLDATETIME,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
AS
BEGIN
	DECLARE @sql nvarchar(4000),
			@paramlist nvarchar(4000)
	SELECT @sql=
	'SELECT 
		tblSynologenOrder.cId,
		tblSynologenOrder.cCompanyUnit,
		tblSynologenOrder.cCustomerFirstName,
		tblSynologenOrder.cCustomerLastName,
		tblSynologenOrder.cCustomerFirstName + '' '' + tblSynologenOrder.cCustomerLastName As cCustomerName,
		tblSynologenOrder.cPersonalIdNumber,
		tblSynologenOrder.cPhone,
		tblSynologenOrder.cEmail,
		tblSynologenOrder.cInvoiceNumber,
		tblSynologenOrder.cStatusId,
		tblSynologenCompany.cName AS cCompanyName,
		(SELECT cName FROM tblSynologenContract WHERE tblSynologenContract.cId = tblSynologenCompany.cContractCustomerId) AS cContractName,
		(SELECT cName FROM tblSynologenOrderStatus WHERE tblSynologenOrderStatus.cId = tblSynologenOrder.cStatusId) AS cStatusName,
		tblSynologenOrder.cCreatedDate,
		dbo.sfSynologenGetFullUserName(tblSynologenOrder.cSalesPersonMemberId) AS cSalesPersonName,
		dbo.sfSynologenGetShopName(tblSynologenOrder.cSalesPersonMemberId) AS cSalesPersonShopName
	FROM tblSynologenOrder
	INNER JOIN tblSynologenCompany ON tblSynologenCompany.cId = tblSynologenOrder.cCompanyId'

		IF (@settlementId IS NOT NULL AND @settlementId > 0) BEGIN
			SELECT @sql = @sql + 
			' INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
			 WHERE tblSynologenSettlementOrderConnection.cSettlementId = @xSettlementId'
		END
		ELSE BEGIN	
			SELECT @sql = @sql + ' WHERE 1=1'
		END

		IF (@contractId IS NOT NULL AND @contractId > 0) BEGIN
			SELECT @sql = @sql + ' AND tblSynologenCompany.cContractCustomerId = @xContractId'
		END
		IF (@statusId IS NOT NULL AND @statusId > 0) BEGIN
			SELECT @sql = @sql + ' AND tblSynologenOrder.cStatusId = @xStatusId'
		END
		IF (@createDateStartLimit IS NOT NULL) BEGIN
			SELECT @sql = @sql + ' AND DATEDIFF(day, @xCreateDateStartLimit, tblSynologenOrder.cCreatedDate) >= 0'
		END
		IF (@createDateStopLimit IS NOT NULL) BEGIN
			SELECT @sql = @sql + ' AND DATEDIFF(day, tblSynologenOrder.cCreatedDate, @xCreateDateStopLimit) >= 0'
		END


		IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
		BEGIN
			SELECT @sql = @sql + 
			' AND (tblSynologenOrder.cId LIKE ''%''+@xSearchString+
			''%''OR tblSynologenOrder.cInvoiceNumber LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cCompanyUnit LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cCustomerFirstName LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cCustomerLastName LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cCustomerFirstName + '' '' + tblSynologenOrder.cCustomerLastName LIKE ''%''+@xSearchString+''%''+
			''%''OR dbo.sfSynologenGetFullUserName(tblSynologenOrder.cSalesPersonMemberId)  LIKE ''%''+@xSearchString+''%''+
			''%''OR dbo.sfSynologenGetShopName(tblSynologenOrder.cSalesPersonMemberId) LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenCompany.cName LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cPhone LIKE ''%''+@xSearchString+''%''+
			''%''OR tblSynologenOrder.cEmail LIKE ''%''+@xSearchString+''%'')'
		END
		IF (@OrderBy IS NOT NULL AND LEN(@OrderBy) > 0)
		BEGIN
			SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
		END
		SELECT @paramlist =	   '@xContractId INT,
								@xStatusId INT,
								@xSettlementId INT,
								@xCreateDateStartLimit SMALLDATETIME, 
								@xCreateDateStopLimit SMALLDATETIME,
								@xSearchString NVARCHAR(255)'
		EXEC sp_executesql @sql,
							@paramlist, 
							@contractId,
							@statusId,
							@settlementId,
							@createDateStartLimit,
							@createDateStopLimit,
							@SearchString

END
