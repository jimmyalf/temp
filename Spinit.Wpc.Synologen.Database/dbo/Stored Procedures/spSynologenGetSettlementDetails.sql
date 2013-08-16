CREATE PROCEDURE spSynologenGetSettlementDetails
					@settlementId INT,
					@orderBy NVARCHAR(255),
					@settlementValueIncludingVAT FLOAT OUTPUT,
					@settlementValueExcludingVAT FLOAT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT
			tblSynologenSettlement.cId
			,tblSynologenSettlement.cCreatedDate
			,tblSynologenShop.cId cShopId
			,tblSynologenShop.cShopNumber
			,tblSynologenShop.cShopName
			,tblSynologenShop.cGiroNumber
			,SUM(tblSynologenOrder.cInvoiceSumExcludingVAT) cPriceExcludingVAT
			,SUM(tblSynologenOrder.cInvoiceSumIncludingVAT) cPriceIncludingVAT
			,COUNT(tblSynologenOrder.cId) cNumberOfOrders
		 FROM tblSynologenSettlementOrderConnection
			 INNER JOIN tblSynologenSettlement ON tblSynologenSettlement.cId = tblSynologenSettlementOrderConnection.cSettlementId
			 INNER JOIN tblSynologenOrder ON tblSynologenOrder.cId = tblSynologenSettlementOrderConnection.cOrderId
			 INNER JOIN tblSynologenShop ON tblSynologenShop.cId = tblSynologenOrder.cSalesPersonShopId
		 WHERE 1=1'
		 
	IF (@settlementId IS NOT NULL AND @settlementId>0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenSettlementOrderConnection.cSettlementId = @xSettlementId'
	END			

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	
	SELECT @sql = @sql + ' GROUP BY 
			tblSynologenShop.cId
			,tblSynologenShop.cShopNumber
			,tblSynologenShop.cShopName
			,tblSynologenShop.cGiroNumber
			,tblSynologenSettlement.cId
			,tblSynologenSettlement.cCreatedDate'
			
	SELECT @paramlist = '@xSettlementId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@settlementId
						
	SELECT @settlementValueIncludingVAT = dbo.sfSynologenGetSettlementOrderValue(@settlementId,0,1)
	SELECT @settlementValueExcludingVAT = dbo.sfSynologenGetSettlementOrderValue(@settlementId,0,0)	
									

END
