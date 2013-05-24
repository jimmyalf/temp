CREATE PROCEDURE spCommerceSearchProductPrice
					@type INT,
					@prdId INT,
					@crnCde NVARCHAR (10),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cPrdId,
					cCrnCde,
					cPrice
			FROM	tblCommerceProductPrice
			ORDER BY cPrdId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cPrdId,
					cCrnCde,
					cPrice
			FROM	tblCommerceProductPrice
			WHERE	cPrdId = @prdId
			ORDER BY cPrdId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cPrdId,
					cCrnCde,
					cPrice
			FROM	tblCommerceProductPrice
			WHERE	cPrdId = @prdId
				AND	cCrnCde = @crnCde
			ORDER BY cPrdId ASC
		END

	SET @status = @@ERROR
			
END
