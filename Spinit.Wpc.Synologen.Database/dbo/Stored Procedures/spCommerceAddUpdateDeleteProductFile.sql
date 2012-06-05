CREATE PROCEDURE spCommerceAddUpdateDeleteProductFile
			@action INT, 
			@productId INT, 
			@fileId INT, 
			@languageId INT,
			@categoryId INT = NULL,  
			@name NVARCHAR(50) = NULL,
			@description NVARCHAR(512) = NULL,
			@order INT = 0,
			@status INT OUTPUT
AS BEGIN
	DECLARE @dummy INT
	
	--VALIDATION
	IF (@action = 0) BEGIN
		SELECT	@dummy = 1 FROM	tblCommerceProductFile WHERE cPrdId = @productId AND cFleId = @fileId AND cLngId = @languageId			
		IF @@ROWCOUNT > 0 BEGIN
			SET @status = -2
			RETURN
		END		
	END
	ELSE IF (@action = 0 OR @action = 1) BEGIN		
		SELECT	@dummy = 1 FROM	tblCommerceProduct WHERE cId = @productId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -10
			RETURN
		END
		
		SELECT	@dummy = 1 FROM	tblBaseFile WHERE cId = @fileId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -8
			RETURN
		END
		
		SELECT	@dummy = 1 FROM	tblBaseLanguages WHERE cId = @languageId
		IF @@ROWCOUNT = 0 BEGIN 
			SET @status = -5
			RETURN
		END
	END
	ELSE IF @action = 2 BEGIN
		SELECT	@dummy = 1 FROM	tblCommerceProductFile WHERE cPrdId = @productId AND cFleId = @fileId AND cLngId = @languageId			
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -3
			RETURN
		END
	END 
	
	--CREATE				
	IF @action = 0 BEGIN
		DECLARE @newOrder INT
		SELECT @newOrder = MAX (cOrder) + 1 FROM tblCommerceProductFile
		IF (@newOrder IS NULL) BEGIN
			SET @newOrder = 1
		END
	
		INSERT INTO tblCommerceProductFile (cPrdId, cFleId, cLngId, cProductFileCategoryId, cName, cDescription, cOrder)
		VALUES (@productId, @fileId, @languageId, @categoryId, @name, @description, @newOrder)
	END
	--UPDATE
	ELSE IF @action = 1 BEGIN	
		UPDATE tblCommerceProductFile 
			SET cProductFileCategoryId  = @categoryId, cName = @name, cDescription = @description, cOrder = @order
		WHERE cPrdId = @productId AND cFleId = @fileId AND cLngId = @languageId
	END	
	--DELETE
	ELSE IF @action = 2 BEGIN			
		DELETE FROM tblCommerceProductFile
		WHERE cPrdId = @productId AND cFleId = @fileId AND cLngId = @languageId
	END
	ELSE BEGIN
		SET @status = -1
		RETURN
	END
	
	SET @status = @@ERROR
END
