CREATE PROCEDURE spCommerceSearchProductFile
					@type INT,
					@prdId INT,
					@fleId INT,
					@lngId INT,
					@productFileCategory INT = NULL,
					@status INT OUTPUT
AS
BEGIN
	IF (@productFileCategory IS NULL) BEGIN
		IF @type = 0 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			ORDER BY cOrder ASC
		END
			
		IF @type = 1 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId
			ORDER BY cOrder ASC
		END						

		IF @type = 2 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId AND	cFleId = @fleId AND	cLngId = @lngId 
			ORDER BY cOrder ASC
		END
	END
	ELSE BEGIN
		IF @type = 0 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			WHERE cProductFileCategoryId = @productFileCategory
			ORDER BY cOrder ASC
		END
			
		IF @type = 1 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId AND cProductFileCategoryId = @productFileCategory
			ORDER BY cOrder ASC
		END						

		IF @type = 2 BEGIN
			SELECT	cPrdId,cFleId,cLngId,cProductFileCategoryId,cName,cDescription,cOrder
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId AND	cFleId = @fleId AND	cLngId = @lngId AND cProductFileCategoryId = @productFileCategory
			ORDER BY cOrder ASC
		END
	END	

	SET @status = @@ERROR
			
END
