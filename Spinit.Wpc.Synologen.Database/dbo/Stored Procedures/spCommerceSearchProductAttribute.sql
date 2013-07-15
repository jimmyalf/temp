CREATE PROCEDURE spCommerceSearchProductAttribute
					@type INT,
					@prdId INT,
					@attId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductAttribute
			ORDER BY cPrdId ASC, cOrder ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
			ORDER BY cPrdId ASC, cOrder ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductAttribute
			WHERE	cAttId = @attId
			ORDER BY cPrdId ASC, cOrder ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cPrdId,
					cAttId,
					cOrder,
					cValue
			FROM	tblCommerceProductAttribute
			WHERE	cPrdId = @prdId
				AND	cAttId = @attId
			ORDER BY cPrdId ASC, cOrder ASC
		END

	SET @status = @@ERROR
			
END
