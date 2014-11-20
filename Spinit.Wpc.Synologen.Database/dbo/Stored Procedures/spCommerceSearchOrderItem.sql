CREATE PROCEDURE spCommerceSearchOrderItem
					@type INT,
					@id INT,
					@prdId INT,
					@ordId INT,
					@ordStsId INT,
					@from DATETIME,
					@to DATETIME,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	tblCommerceOrderItem.cId,
					tblCommerceOrderItem.cOrdStsId,
					tblCommerceOrderItem.cOrdId,
					tblCommerceOrderItem.cPrdId,
					tblCommerceOrderItem.cNoOfProducts,
					tblCommerceOrderItem.cPrice,
					tblCommerceOrderItem.cSum,
					tblCommerceOrderItem.cCrnCde
			FROM	tblCommerceOrderItem
				INNER JOIN tblCommerceOrder
					ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
			ORDER BY cCustomerId ASC, cOrdId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cOrdId,
					cPrdId,
					cNoOfProducts,
					cPrice,
					cSum,
					cCrnCde
			FROM	tblCommerceOrderItem
			WHERE	cPrdId = @prdId
			ORDER BY cId ASC
		END

	IF @type = 2
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cOrdId,
					cPrdId,
					cNoOfProducts,
					cPrice,
					cSum,
					cCrnCde
			FROM	tblCommerceOrderItem
			WHERE	cOrdId = @ordId
			ORDER BY cId ASC
		END

	IF @type = 3
		BEGIN
			IF @ordStsId IS NOT NULL
				BEGIN
					IF (@from IS NOT NULL) AND (@to IS NOT NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrdStsId = @ordStsId
													AND	cOrderDate BETWEEN @from AND @to)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NOT NULL) AND (@to IS NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrdStsId = @ordStsId
													AND	cOrderDate >= @from)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NULL) AND (@to IS NOT NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrdStsId = @ordStsId
													AND	cOrderDate <= @to)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NULL) AND (@to IS NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrdStsId = @ordStsId)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END
				END
			ELSE
				BEGIN
					IF (@from IS NOT NULL) AND (@to IS NOT NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrderDate BETWEEN @from AND @to)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NOT NULL) AND (@to IS NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrderDate >= @from)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NULL) AND (@to IS NOT NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							WHERE	cOrdId IN	(SELECT	cId
												 FROM	tblCommerceOrder
												 WHERE	cOrderDate <= @to)
							ORDER BY cCustomerId ASC, cOrdId ASC
						END

					IF (@from IS NULL) AND (@to IS NULL)
						BEGIN
							SELECT	tblCommerceOrderItem.cId,
									tblCommerceOrderItem.cOrdStsId,
									tblCommerceOrderItem.cOrdId,
									tblCommerceOrderItem.cPrdId,
									tblCommerceOrderItem.cNoOfProducts,
									tblCommerceOrderItem.cPrice,
									tblCommerceOrderItem.cSum,
									tblCommerceOrderItem.cCrnCde
							FROM	tblCommerceOrderItem
								INNER JOIN tblCommerceOrder
									ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
							ORDER BY cCustomerId ASC, cOrdId ASC
						END
				END
		END

	IF @type = 4
		BEGIN
			SELECT	cId,
					cOrdStsId,
					cOrdId,
					cPrdId,
					cNoOfProducts,
					cPrice,
					cSum,
					cCrnCde
			FROM	tblCommerceOrderItem
			WHERE	cId = @id
			ORDER BY cId ASC
		END

	SET @status = @@ERROR		
END
