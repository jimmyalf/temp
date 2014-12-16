CREATE PROCEDURE spCommerceSearchProductCategoryAttribute
					@type INT,
					@prdCatId INT,
					@attId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdCatId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductCategoryAttribute
			ORDER BY cPrdCatId ASC, cOrder ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdCatId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC, cOrder ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdCatId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cAttId = @attId
			ORDER BY cPrdCatId ASC, cOrder ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cPrdCatId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductCategoryAttribute
			WHERE	cPrdCatId = @prdCatId
				AND	cAttId = @attId
			ORDER BY cPrdCatId ASC, cOrder ASC
		END

	SET @status = @@ERROR
			
END
