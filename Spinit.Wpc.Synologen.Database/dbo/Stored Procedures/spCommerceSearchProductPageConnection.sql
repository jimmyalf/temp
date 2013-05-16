CREATE PROCEDURE [dbo].[spCommerceSearchProductPageConnection]
					@type INT,
					@prdId INT,
					@pgeId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdId,
					cPgeId
			FROM	tblCommerceProductPageConnection
			ORDER BY cPrdId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdId,
					cPgeId
			FROM	tblCommerceProductPageConnection
			WHERE	cPrdId = @prdId
			ORDER BY cPrdId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdId,
					cPgeId
			FROM	tblCommerceProductPageConnection
			WHERE	cPgeId = @pgeId
			ORDER BY cPrdId ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cPrdId,
					cPgeId
			FROM	tblCommerceProductPageConnection
			WHERE	cPrdId = @prdId
				AND	cPgeId = @pgeId
			ORDER BY cPrdId ASC
		END

	SET @status = @@ERROR
			
END
