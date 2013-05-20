CREATE PROCEDURE spCommerceAddUpdateDeleteCurrency
					@action INT,
					@currencyCode NVARCHAR(10),
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN		
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @currencyCode
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END
			
			INSERT INTO tblCommerceCurrency
				(cCurrencyCode, cName)
			VALUES
				(@currencyCode, @name)
				
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @currencyCode
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			UPDATE	tblCommerceCurrency
			SET		cName = @name
			WHERE	cCurrencyCode = @currencyCode
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceCurrency
			WHERE	cCurrencyCode = @currencyCode
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceCurrency
			WHERE		cCurrencyCode = @currencyCode
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
