CREATE PROCEDURE spCommerceAddUpdateDeleteProductPrice
					@action INT,
					@prdId INT,
					@crnCde NVARCHAR (10),
					@price MONEY,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductPrice
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
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
			
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -11
					RETURN
				END
			
			INSERT INTO tblCommerceProductPrice
				(cPrdId, cCrnCde, cPrice)
			VALUES
				(@prdId, @crnCde, @price)
		END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductPrice
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
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
			
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -11
					RETURN
				END

			UPDATE	tblCommerceProductPrice
			SET		cPrice = @price
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductPrice
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductPrice
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
