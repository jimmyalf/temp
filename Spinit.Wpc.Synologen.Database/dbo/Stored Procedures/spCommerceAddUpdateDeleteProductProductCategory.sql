CREATE PROCEDURE spCommerceAddUpdateDeleteProductProductCategory
					@action INT,
					@prdId INT,
					@prdCatId INT,
					@order INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0 BEGIN	
		SELECT	@dummy = 1 FROM tblCommerceProductProductCategory WHERE cPrdId = @prdId AND	cPrdCatId = @prdCatId
		IF @@ROWCOUNT > 0 BEGIN
			SET @status = -2
			RETURN
		END		
		
		SELECT @dummy = 1 FROM tblCommerceProduct WHERE cId = @prdId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -10
			RETURN
		END
		
		SELECT @dummy = 1 FROM tblCommerceProductCategory WHERE cId = @prdCatId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -13
			RETURN
		END
		
		IF (@order <= 1) BEGIN
			SELECT @order = MAX(cOrder)+1 FROM tblCommerceProductProductCategory WHERE cPrdCatId = @prdCatId
		END
		IF (@order IS NULL)BEGIN
			SET @order = 1
		END
		
		INSERT INTO tblCommerceProductProductCategory(cPrdId, cPrdCatId, cOrder)
		VALUES(@prdId, @prdCatId, @order)
	END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductProductCategory
			WHERE	cPrdId = @prdId
				AND	cPrdCatId = @prdCatId
			
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
			FROM	tblCommerceProductCategory
			WHERE	cId = @prdCatId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -13
					RETURN
				END
				
			UPDATE	tblCommerceProductProductCategory
			SET		cOrder = @order
			WHERE	cPrdId = @prdId
				AND	cPrdCatId = @prdCatId
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductProductCategory
			WHERE	cPrdId = @prdId
				AND	cPrdCatId = @prdCatId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductProductCategory
			WHERE	cPrdId = @prdId
				AND	cPrdCatId = @prdCatId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
