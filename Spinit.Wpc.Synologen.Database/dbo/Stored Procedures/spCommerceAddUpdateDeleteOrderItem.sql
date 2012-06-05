CREATE PROCEDURE spCommerceAddUpdateDeleteOrderItem
					@action INT,
					@id INT OUTPUT,
					@ordStsId INT,
					@ordId INT,
					@prdId INT,
					@noOfProducts INT,
					@price MONEY,
					@sum MONEY,
					@crnCde NVARCHAR (10),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
										
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
			FROM	tblCommerceOrder
			WHERE	cId = @ordId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -19
					RETURN
				END
				
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -11
					RETURN
				END
			
			SELECT	@dummy = 1
			FROM	tblCommerceProduct
			WHERE	cId = @prdId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -10
					RETURN
				END
							
			INSERT INTO tblCommerceOrderItem
				(cOrdStsId, cOrdId, cPrdId, cNoOfProducts,
				 cPrice, cSum, cCrnCde)
			VALUES
				(@ordStsId, @ordId, @prdId, @noOfProducts,
				 @price, @sum, @crnCde)
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceOrderItem
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			SELECT	@dummy = 1
			FROM	tblCommerceOrder
			WHERE	cId = @ordId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -19
					RETURN
				END
				
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -11
					RETURN
				END
			
			SELECT	@dummy = 1
			FROM	tblCommerceProduct
			WHERE	cId = @prdId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -10
					RETURN
				END

			UPDATE	tblCommerceOrderItem
			SET		cOrdStsId = @ordStsId,
					cOrdId = @ordId,
					cPrdId = @prdId,
					cNoOfProducts = @noOfProducts,
					cPrice = @price,
					cSum = @sum,
					cCrnCde = @crnCde
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceOrderItem
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceOrderItem
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
