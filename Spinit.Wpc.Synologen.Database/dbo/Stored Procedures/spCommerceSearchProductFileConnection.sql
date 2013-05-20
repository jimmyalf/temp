CREATE PROCEDURE [dbo].[spCommerceSearchProductFileConnection]
					@type INT,
					@prdId INT,
					@fleId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdId,
					cFleId
			FROM	tblCommerceProductFileConnection
			ORDER BY cPrdId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdId,
					cFleId
			FROM	tblCommerceProductFileConnection
			WHERE	cPrdId = @prdId
			ORDER BY cPrdId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdId,
					cFleId
			FROM	tblCommerceProductFileConnection
			WHERE	cFleId = @fleId
			ORDER BY cPrdId ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cPrdId,
					cFleId
			FROM	tblCommerceProductFileConnection
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
			ORDER BY cPrdId ASC
		END

	SET @status = @@ERROR
			
END
