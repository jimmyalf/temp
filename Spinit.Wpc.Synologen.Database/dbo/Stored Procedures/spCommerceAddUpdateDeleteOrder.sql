CREATE PROCEDURE spCommerceAddUpdateDeleteOrder
					@action INT,
					@id INT OUTPUT,
					@ordStsId INT,
					@customerId INT,
					@customerType INT,
					@sum MONEY,
					@payTpeId INT,
					@handledBy NVARCHAR (100),
					@delTpeId INT,
					@deliveryDate DATETIME,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT,
			@ordHandledBy NVARCHAR (100),
			@ordHandledDate DATETIME
			
	SET @ordHandledDate = NULL
							
	IF @action = 0
		BEGIN		
			SELECT	@dummy = 1
			FROM	tblCommerceOrderStatus
			WHERE	cId = @ordStsId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -14
					RETURN
				END
							
			SELECT	@dummy = 1
			FROM	tblCommercePaymentType
			WHERE	cId = @payTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -17
					RETURN
				END
				
			SELECT	@dummy = 1
			FROM	tblCommerceDeliveryType
			WHERE	cId = @delTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -18
					RETURN
				END
			
			IF @handledBy IS NOT NULL
				BEGIN
					SET @ordHandledDate = GETDATE ()
				END
				
			INSERT INTO tblCommerceOrder
				(cOrdStsId, cCustomerId, cCustomerType, cSum, cOrderDate, cPayTpeId,
				 cHandledBy, cHandledDate, cDelTpeId, cDeliveryDate)
			VALUES
				(@ordStsId, @customerId, @customerType, @sum, GETDATE (), @payTpeId,
				 @handledBy, @ordHandledDate, @delTpeId, @deliveryDate)
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@ordHandledBy = cHandledBy,
					@ordHandledDate = cHandledDate
			FROM	tblCommerceOrder
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			IF @handledBy IS NOT NULL
				BEGIN
					IF @ordHandledBy IS NOT NULL
						BEGIN
							IF @handledBy <> @ordHandledBy
								BEGIN
									SET @ordHandledDate = GETDATE ()
								END
						END
					ELSE
						BEGIN
							SET @ordHandledDate = GETDATE ()
						END
				END
			ELSE
				BEGIN
					SET @ordHandledDate = NULL
				END
							
			SELECT	@dummy = 1
			FROM	tblCommerceOrderStatus
			WHERE	cId = @ordStsId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -14
					RETURN
				END
			
			SELECT	@dummy = 1
			FROM	tblCommercePaymentType
			WHERE	cId = @payTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -17
					RETURN
				END
				
			SELECT	@dummy = 1
			FROM	tblCommerceDeliveryType
			WHERE	cId = @delTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -18
					RETURN
				END
			
			UPDATE	tblCommerceOrder
			SET		cOrdStsId = @ordStsId,
					cCustomerId = @customerId,
					cCustomerType = @customerType,
					cSum = @sum,
					cPayTpeId = @payTpeId,
					cHandledBy = @handledBy,
					cHandledDate = @ordHandledDate,
					cDelTpeId = @delTpeId,
					cDeliveryDate = @deliveryDate
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceOrder
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceOrderItem
			WHERE		cOrdId = @id
				
			DELETE FROM tblCommerceOrder
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
