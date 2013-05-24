create FUNCTION sfSynologenGetSettlementIsMarkedAsPayed (@settlementId INT, @shopId INT)
	RETURNS BIT
AS
BEGIN
	DECLARE @OrdersNotMarkedAsPayed INT
	DECLARE @returnBit BIT
	
	--Search for both shop and settlement orders
	IF (@settlementId IS NOT NULL AND @settlementId > 0 AND @shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT @OrdersNotMarkedAsPayed = COUNT(cId)
		FROM tblSynologenOrder
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenOrder.cSalesPersonShopId = @shopId
			AND tblSynologenSettlementOrderConnection.cSettlementId = @settlementId
			AND tblSynologenOrder.cOrderMarkedAsPayed = 0
	END
	
	--Search for specific settlement orders
	ELSE IF (@settlementId IS NOT NULL AND @settlementId > 0) BEGIN
		SELECT @OrdersNotMarkedAsPayed = COUNT(cId)
		FROM tblSynologenOrder
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenSettlementOrderConnection.cSettlementId = @settlementId
			AND tblSynologenOrder.cOrderMarkedAsPayed = 0
	END
	
	--Search for specific shop orders
	ELSE IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT @OrdersNotMarkedAsPayed = COUNT(cId)
		FROM tblSynologenOrder
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenOrder.cSalesPersonShopId = @shopId
			AND tblSynologenOrder.cOrderMarkedAsPayed = 0
	END
	
	--Search for all settlement orders
	ELSE BEGIN
		SELECT @OrdersNotMarkedAsPayed = COUNT(cId)
		FROM tblSynologenOrder
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenOrder.cOrderMarkedAsPayed = 0
	END	
	
	--Set return value and return
	IF (@OrdersNotMarkedAsPayed>0) BEGIN
		SET @returnBit= 0
	END
	ELSE BEGIN
		SET @returnBit = 1
	END
	RETURN @returnBit
END
