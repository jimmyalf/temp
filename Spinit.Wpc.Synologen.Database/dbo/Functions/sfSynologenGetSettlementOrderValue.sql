create FUNCTION sfSynologenGetSettlementOrderValue (@settlementId INT, @shopId INT, @includeVAT BIT)
	RETURNS FLOAT
AS
BEGIN
	DECLARE @valueIncludingVAT FLOAT
	DECLARE @valueExcludingVAT FLOAT
	DECLARE @returnValue FLOAT
	
	--Search for both shop and settlement orders
	IF (@settlementId IS NOT NULL AND @settlementId > 0 AND @shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT 
			@valueIncludingVAT = SUM(cInvoiceSumIncludingVAT),
			@valueExcludingVAT = SUM(cInvoiceSumExcludingVAT)
		FROM tblSynologenOrder 
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE 
			tblSynologenSettlementOrderConnection.cSettlementId = @settlementId
			AND tblSynologenOrder.cSalesPersonShopId = @shopId
	END
	
	--Search for specific settlement orders
	ELSE IF (@settlementId IS NOT NULL AND @settlementId > 0) BEGIN
		SELECT 
			@valueIncludingVAT = SUM(cInvoiceSumIncludingVAT),
			@valueExcludingVAT = SUM(cInvoiceSumExcludingVAT)
		FROM tblSynologenOrder 
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE 
			tblSynologenSettlementOrderConnection.cSettlementId = @settlementId
	END
	
	--Search for specific shop orders
	ELSE IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT 
			@valueIncludingVAT = SUM(cInvoiceSumIncludingVAT),
			@valueExcludingVAT = SUM(cInvoiceSumExcludingVAT)
		FROM tblSynologenOrder 
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenOrder.cSalesPersonShopId = @shopId
	END
	
	--Search for all settlement orders
	ELSE BEGIN
		SELECT 
			@valueIncludingVAT = SUM(cInvoiceSumIncludingVAT),
			@valueExcludingVAT = SUM(cInvoiceSumExcludingVAT)
		FROM tblSynologenOrder 
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
	END	
	
	--Set return value and return
	IF (@includeVAT>0) BEGIN
		SET @returnValue = @valueIncludingVAT
	END
	ELSE BEGIN
		SET @returnValue = @valueExcludingVAT
	END
	RETURN @returnValue
END
