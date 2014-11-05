CREATE PROCEDURE [dbo].[spCommerceSearchProductProductCategory]
					@type INT,
					@prdId INT,
					@prdCatId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdId,
					cPrdCatId,
					cOrder
			FROM	tblCommerceProductProductCategory
			ORDER BY cPrdCatId ASC, cOrder ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdId,
					cPrdCatId,
					cOrder
			FROM	tblCommerceProductProductCategory
			WHERE	cPrdId = @prdId
			ORDER BY cOrder ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdId,
					cPrdCatId,
					cOrder
			FROM	tblCommerceProductProductCategory
			WHERE	cPrdCatId = @prdCatId
			ORDER BY cOrder ASC
		END						

	IF @type = 3
		BEGIN
			SELECT	cPrdId,
					cPrdCatId,
					cOrder
			FROM	tblCommerceProductProductCategory
			WHERE	cPrdId = @prdId
				AND	cPrdCatId = @prdCatId
			ORDER BY cOrder ASC
		END

	SET @status = @@ERROR
			
END
