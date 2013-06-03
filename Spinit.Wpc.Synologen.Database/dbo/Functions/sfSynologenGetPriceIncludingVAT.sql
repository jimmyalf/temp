create FUNCTION sfSynologenGetPriceIncludingVAT (@price FLOAT, @noVAT BIT, @VAT FLOAT)
	RETURNS FLOAT AS BEGIN
		
		DECLARE @returnValue FLOAT
		IF (@noVAT = 1) BEGIN
			SET @returnValue = @price
		END
		ELSE BEGIN
			SET @returnValue = @price * (1 + @VAT)
		END
		RETURN @returnValue
	END
