CREATE PROCEDURE spSynologenAddSettlement
		@filterStatusId INT,
		@statusIdAfterSettlement INT,
		@status INT OUTPUT,
		@settlementId INT OUTPUT,
		@numberOfOrdersAdded INT OUTPUT
		
AS BEGIN TRANSACTION ADD_SYNOLOGEN_SETTLEMENT

	DECLARE @orderId INT
	
	--Create Settlement
	INSERT INTO tblSynologenSettlement(cCreatedDate) VALUES (GETDATE())
	SELECT @settlementId = @@IDENTITY	
	
	DECLARE getOrders CURSOR LOCAL FOR
	SELECT cId FROM tblSynologenOrder WHERE cStatusId = @filterStatusId

	OPEN getOrders
	FETCH NEXT FROM getOrders INTO @orderId
	
	WHILE (@@FETCH_STATUS <> -1) BEGIN
		--Create Settlement-Order connection
		INSERT INTO tblSynologenSettlementOrderConnection(cSettlementId,cOrderId) VALUES (@settlementId,@orderId)
		--Update Order status id
		UPDATE tblSynologenOrder 
			SET cStatusId = @statusIdAfterSettlement,
			cUpdatedDate = GETDATE()
			WHERE cId = @orderId
		SELECT @numberOfOrdersAdded = @numberOfOrdersAdded + 1
		FETCH NEXT FROM getOrders INTO @orderId
	END
	CLOSE getOrders
	DEALLOCATE getOrders
	
	SELECT @status = @@ERROR
	IF (@@ERROR <> 0) BEGIN
		SELECT @numberOfOrdersAdded = 0
		SELECT @settlementId = 0
		ROLLBACK TRANSACTION ADD_SYNOLOGEN_SETTLEMENT
		RETURN
	END
								
	IF (@@ERROR = 0) BEGIN
		COMMIT TRANSACTION ADD_SYNOLOGEN_SETTLEMENT
	END
