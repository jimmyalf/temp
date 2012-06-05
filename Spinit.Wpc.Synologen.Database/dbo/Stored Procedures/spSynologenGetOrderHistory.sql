CREATE PROCEDURE spSynologenGetOrderHistory
	@orderId INT,
	@status INT OUTPUT
AS BEGIN
	SELECT	*
		FROM tblSynologenOrderHistory
		WHERE cOrderId = @orderId
		ORDER BY cCreatedDate,cId ASC
	SELECT @status = @@ERROR
END
