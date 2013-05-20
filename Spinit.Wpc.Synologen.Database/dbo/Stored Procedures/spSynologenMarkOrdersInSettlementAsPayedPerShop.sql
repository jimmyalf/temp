create PROCEDURE spSynologenMarkOrdersInSettlementAsPayedPerShop
		@settlementId INT,
		@shopId INT,
		@status INT OUTPUT
				
AS BEGIN TRANSACTION UPDATE_PAYED_IN_SETTLEMENT

	DECLARE @orderId INT
	DECLARE getOrders CURSOR LOCAL FOR
		SELECT cId 
		FROM tblSynologenOrder 
			INNER JOIN tblSynologenSettlementOrderConnection ON tblSynologenSettlementOrderConnection.cOrderId = tblSynologenOrder.cId
		WHERE tblSynologenSettlementOrderConnection.cSettlementId = @settlementId
			AND tblSynologenOrder.cSalesPersonShopId = @shopId

	OPEN getOrders
	FETCH NEXT FROM getOrders INTO @orderId
	
	WHILE (@@FETCH_STATUS <> -1) BEGIN
		--Update Order cOrderMarkedAsPayed BIT
		UPDATE tblSynologenOrder SET cOrderMarkedAsPayed = 1 WHERE cId = @orderId
		--SELECT @numberOfOrdersAdded = @numberOfOrdersAdded + 1
		FETCH NEXT FROM getOrders INTO @orderId
	END
	CLOSE getOrders
	DEALLOCATE getOrders
	
	SELECT @status = @@ERROR
	IF (@@ERROR <> 0) BEGIN
		ROLLBACK TRANSACTION UPDATE_PAYED_IN_SETTLEMENT
		RETURN
	END
								
	IF (@@ERROR = 0) BEGIN
		COMMIT TRANSACTION UPDATE_PAYED_IN_SETTLEMENT
	END
RETURN
