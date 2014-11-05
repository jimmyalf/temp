CREATE PROCEDURE [dbo].[spCommerceSearchProductCategoryFile]
					@type INT,
					@prdCatId INT,
					@fleId INT,
					@lngId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdCatId,
					cFleId,
					cLngId
			FROM	tblCommerceProductCategoryFile
			ORDER BY cPrdCatId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdCatId,
					cFleId,
					cLngId
			FROM	tblCommerceProductCategoryFile
			WHERE	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdCatId,
					cFleId,
					cLngId
			FROM	tblCommerceProductCategoryFile
			WHERE	cPrdCatId = @prdCatId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
			ORDER BY cPrdCatId ASC
		END

	SET @status = @@ERROR
			
END
