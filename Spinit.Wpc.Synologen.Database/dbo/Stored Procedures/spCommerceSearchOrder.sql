CREATE PROCEDURE spCommerceSearchOrder
					@type INT,
					@id INT,
					@prdId INT,
					@customerId INT,
					@ordStsId INT,
					@from DATETIME,
					@to DATETIME,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			IF (@from IS NOT NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrderDate BETWEEN @from AND @to
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NOT NULL) AND (@to IS NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrderDate >= @from
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrderDate <= @to
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NULL) AND (@to IS NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
					ORDER BY tblCommerceOrder.cId ASC
				END
		END
		
	IF @type = 1
		BEGIN
			IF (@from IS NOT NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
						INNER JOIN tblCommerceOrderItem
							ON tblCommerceOrderItem.cOrdId = tblCommerceOrder.cId
					WHERE	tblCommerceOrderItem.cPrdId = @prdId
						AND	cOrderDate BETWEEN @from AND @to
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NOT NULL) AND (@to IS NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
						INNER JOIN tblCommerceOrderItem
							ON tblCommerceOrderItem.cOrdId = tblCommerceOrder.cId
					WHERE	tblCommerceOrderItem.cPrdId = @prdId
						AND cOrderDate >= @from
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
						INNER JOIN tblCommerceOrderItem
							ON tblCommerceOrderItem.cOrdId = tblCommerceOrder.cId
					WHERE	tblCommerceOrderItem.cPrdId = @prdId
						AND cOrderDate <= @to
					ORDER BY tblCommerceOrder.cId ASC
				END
			
			IF (@from IS NULL) AND (@to IS NULL)
				BEGIN
					SELECT	tblCommerceOrder.cId,
							tblCommerceOrder.cOrdStsId,
							tblCommerceOrder.cCustomerId,
							tblCommerceOrder.cCustomerType,
							tblCommerceOrder.cSum,
							tblCommerceOrder.cOrderDate,
							tblCommerceOrder.cPayTpeId,
							tblCommerceOrder.cHandledBy,
							tblCommerceOrder.cHandledDate,
							tblCommerceOrder.cDelTpeId,
							tblCommerceOrder.cDeliveryDate
					FROM	tblCommerceOrder
						INNER JOIN tblCommerceOrderItem
							ON tblCommerceOrderItem.cOrdId = tblCommerceOrder.cId
					WHERE	tblCommerceOrderItem.cPrdId = @prdId
					ORDER BY tblCommerceOrder.cId ASC
				END
		END

	IF @type = 2
		BEGIN
			IF (@from IS NOT NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 1 
						AND	cOrderDate BETWEEN @from AND @to
					ORDER BY cId ASC
				END

			IF (@from IS NOT NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 1 
						AND cOrderDate >= @from
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 1 
						AND cOrderDate <= @to
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 1 
					ORDER BY cId ASC
				END
		END

	IF @type = 3
		BEGIN
			IF (@from IS NOT NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 2 
						AND	cOrderDate BETWEEN @from AND @to
					ORDER BY cId ASC
				END
			
			IF (@from IS NOT NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 2 
						AND cOrderDate >= @from
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 2 
						AND cOrderDate <= @to
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cCustomerId = @customerId AND cCustomerType = 2 
					ORDER BY cId ASC
				END
		END

	IF @type = 4
		BEGIN
			IF (@from IS NOT NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrdStsId = @ordStsId
						AND	cOrderDate BETWEEN @from AND @to
					ORDER BY cId ASC
				END

			IF (@from IS NOT NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrdStsId = @ordStsId
						AND cOrderDate >= @from
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NOT NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrdStsId = @ordStsId
						AND cOrderDate <= @to
					ORDER BY cId ASC
				END

			IF (@from IS NULL) AND (@to IS NULL)
				BEGIN
					SELECT	cId,
							cOrdStsId,
							cCustomerId,
							cCustomerType,
							cSum,
							cOrderDate,
							cPayTpeId,
							cHandledBy,
							cHandledDate,
							cDelTpeId,
							cDeliveryDate
					FROM	tblCommerceOrder
					WHERE	cOrdStsId = @ordStsId
					ORDER BY cId ASC
				END
		END

	IF @type = 5
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cCustomerId,
					cCustomerType,
					cSum,
					cOrderDate,
					cPayTpeId,
					cHandledBy,
					cHandledDate,
					cDelTpeId,
					cDeliveryDate
			FROM	tblCommerceOrder
			WHERE	cId = @id
			ORDER BY cId ASC
		END
	
	IF @type = 6
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cCustomerId,
					cCustomerType,
					cSum,
					cOrderDate,
					cPayTpeId,
					cHandledBy,
					cHandledDate,
					cDelTpeId,
					cDeliveryDate
			FROM	tblCommerceOrder
			WHERE	cCustomerId = @customerId AND cCustomerType = 1 AND cOrdStsId = @ordStsId
			ORDER BY cId ASC
		END
	
	IF @type = 7
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cCustomerId,
					cCustomerType,
					cSum,
					cOrderDate,
					cPayTpeId,
					cHandledBy,
					cHandledDate,
					cDelTpeId,
					cDeliveryDate
			FROM	tblCommerceOrder
			WHERE	cCustomerId = @customerId AND cCustomerType = 2 AND cOrdStsId = @ordStsId
			ORDER BY cId ASC
		END
		

	SET @status = @@ERROR
			
END
