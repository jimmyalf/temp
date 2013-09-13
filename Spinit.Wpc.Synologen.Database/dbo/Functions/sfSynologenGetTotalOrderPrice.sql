create FUNCTION sfSynologenGetTotalOrderPrice (@orderId INT, @includingVAT BIT, @VATAmount FLOAT)
	RETURNS FLOAT AS BEGIN
		DECLARE @returnVALUE FLOAT
		IF (@includingVAT = 1) BEGIN
			SELECT 
				@returnVALUE = SUM(dbo.sfSynologenGetPriceIncludingVAT(cSinglePrice, cNoVAT, @VATAmount) * cNumberOfItems)
			FROM tblSynologenOrderItems
			WHERE cOrderId = @orderId
		END
		ELSE BEGIN
			SELECT  @returnVALUE = SUM (cSinglePrice * cNumberOfItems)
			FROM tblSynologenOrderItems
			WHERE cOrderId = @orderId
		END
		RETURN @returnValue
	END
