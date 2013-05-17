CREATE PROCEDURE spSynologenGetSettlements
					@settlementId INT,
					@shopId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	DISTINCT
			tblSynologenSettlement.*,
			(SELECT COUNT (*) FROM tblSynologenSettlementOrderConnection WHERE tblSynologenSettlementOrderConnection.cSettlementId = tblSynologenSettlement.cId) AS cNumberOfOrders,
			(SELECT COUNT (*) FROM SynologenLensSubscriptionTransaction WHERE SynologenLensSubscriptionTransaction.SettlementId = tblSynologenSettlement.cId) AS cNumberOfTransactions
			FROM tblSynologenSettlement'
			
	IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT @sql = @sql + 
		' 		LEFT OUTER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cSettlementId = tblSynologenSettlement.cId
				LEFT OUTER JOIN tblSynologenOrder ON tblSynologenOrder.cId = tblSynologenSettlementOrderConnection.cOrderId
				LEFT OUTER JOIN SynologenLensSubscriptionTransaction ON tblSynologenSettlement.cId = SynologenLensSubscriptionTransaction.SettlementId
				LEFT OUTER JOIN SynologenLensSubscription ON SynologenLensSubscription.Id = SynologenLensSubscriptionTransaction.SubscriptionId
				LEFT OUTER JOIN SynologenLensSubscriptionCustomer ON SynologenLensSubscriptionCustomer.Id = SynologenLensSubscription.CustomerId
		WHERE cSalesPersonShopId = @xShopId OR SynologenLensSubscriptionCustomer.ShopId = @xShopId'
	END
	ELSE BEGIN
		SELECT @sql = @sql + ' WHERE 1=1'
	END
	
	IF (@settlementId IS NOT NULL AND @settlementId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenSettlement.cId = @xSettlementId'
	END	

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	
	SELECT @paramlist = '@xShopId INT,@xSettlementId INT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@shopId,
						@settlementId
				

END
