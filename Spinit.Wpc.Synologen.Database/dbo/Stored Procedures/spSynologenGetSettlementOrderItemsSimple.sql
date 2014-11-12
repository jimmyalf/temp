CREATE PROCEDURE spSynologenGetSettlementOrderItemsSimple
					@shopId INT,
					@settlementId INT,
					@orderBy NVARCHAR(255),
					@allOrdersMarkedAsPayed BIT OUTPUT,
					@orderValueIncludingVAT FLOAT OUTPUT,
					@orderValueExcludingVAT FLOAT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT 
			tblSynologenOrderItems.cArticleId
			,tblSynologenArticle.cArticleNumber
			,tblSynologenArticle.cName AS cArticleName
			,SUM(tblSynologenOrderItems.cNumberOfItems) cNumberOfItems
			,tblSynologenOrderItems.cNoVAT
			,SUM(tblSynologenOrderItems.cSinglePrice * tblSynologenOrderItems.cNumberOfItems) cPriceSummary
		FROM tblSynologenOrderItems
			INNER JOIN tblSynologenOrder ON tblSynologenOrder.cId = tblSynologenOrderItems.cOrderId
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrderItems.cOrderId
			INNER JOIN tblSynologenArticle ON tblSynologenArticle.cId = tblSynologenOrderItems.cArticleId
		WHERE 1=1'
		
	IF (@shopId IS NOT NULL AND @shopId>0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrder.cSalesPersonShopId = @xShopId'
	END		
	
	IF (@settlementId IS NOT NULL AND @settlementId>0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenSettlementOrderConnection.cSettlementId = @xSettlementId'
	END			

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	
	SELECT @sql = @sql + ' GROUP BY cArticleId, tblSynologenArticle.cName,
			tblSynologenArticle.cArticleNumber, tblSynologenOrderItems.cNoVAT'
			
	SELECT @paramlist = '@xShopId INT, @xSettlementId INT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@shopId,
						@settlementId
						
	SELECT @allOrdersMarkedAsPayed = dbo.sfSynologenGetSettlementIsMarkedAsPayed(@settlementId,@shopId)
	SELECT @orderValueIncludingVAT = dbo.sfSynologenGetSettlementOrderValue(@settlementId,@shopId,1)
	SELECT @orderValueExcludingVAT = dbo.sfSynologenGetSettlementOrderValue(@settlementId,@shopId,0)
				

END
