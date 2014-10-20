CREATE PROCEDURE spCommerceAddUpdateDeleteProductAttribute
					@action INT,
					@prdId INT,
					@attId INT,
					@order INT,
					@value NVARCHAR (1024),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT,
			@max INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
			
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
			FROM	tblCommerceAttribute
			WHERE	cId = @attId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -12
					RETURN
				END
			
			SELECT	@max = MAX (cOrder)
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				
			IF @max IS NULL 
				BEGIN
					SET @max = 1
				END

			INSERT INTO tblCommerceProductAttribute
				(cPrdId, cAttId, cOrder, cValue)
			VALUES
				(@prdId, @attId, @max + 1, @value)
		END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
			
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
			FROM	tblCommerceAttribute
			WHERE	cId = @attId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -12
					RETURN
				END

			UPDATE	tblCommerceProductAttribute
			SET		cOrder = @order,
					cValue = @value
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
