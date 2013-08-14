CREATE PROCEDURE [dbo].[spCommerceSearchLanguageConnection]
					@type INT,
					@lngId INT,
					@prdCatId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cLngId,
					cPrdCatId
			FROM	tblCommerceLanguageConnection
			ORDER BY cPrdCatId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cLngId,
					cPrdCatId
			FROM	tblCommerceLanguageConnection
			WHERE	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cLngId,
					cPrdCatId
			FROM	tblCommerceLanguageConnection
			WHERE	cLngId = @lngId
				AND	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC
		END

	SET @status = @@ERROR
			
END
