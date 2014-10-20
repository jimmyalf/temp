CREATE PROCEDURE spCommerceAddUpdateDeleteProductCategoryAttribute
					@action INT,
					@prdCatId INT,
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
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END		
			
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategory
			WHERE	cId = @prdCatId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -13
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
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				
			IF @max IS NULL 
				BEGIN
					SET @max = 1
				END

			INSERT INTO tblCommerceProductCategoryAttribute
				(cPrdCatId, cAttId, cOrder, cValue)
			VALUES
				(@prdCatId, @attId, @max + 1, @value)
		END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			SELECT	@dummy = 1
			FROM	tblCommerceProductCategory
			WHERE	cId = @prdCatId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -13
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

			UPDATE	tblCommerceProductCategoryAttribute
			SET		cOrder = @order,
					cValue = @value
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
