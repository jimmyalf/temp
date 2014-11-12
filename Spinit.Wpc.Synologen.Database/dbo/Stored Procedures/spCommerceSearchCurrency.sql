CREATE PROCEDURE spCommerceSearchCurrency
					@type INT,
					@crnCde NVARCHAR(10),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cCurrencyCode,
					cName
			FROM	tblCommerceCurrency
			ORDER BY cName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cCurrencyCode,
					cName
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @crnCde
			ORDER BY cName ASC
		END
						
	SET @status = @@ERROR
			
END
