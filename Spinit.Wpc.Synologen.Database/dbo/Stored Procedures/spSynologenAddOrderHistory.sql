CREATE PROCEDURE spSynologenAddOrderHistory
		@orderId INT,
		@invoiceNumber BIGINT,
		@message NVARCHAR(500) = '',
		@status INT OUTPUT,
		@id INT OUTPUT

AS BEGIN TRANSACTION ADD_SYNOLOGEN_ORDERHISTORY

	IF (@invoiceNumber IS NOT NULL AND @invoiceNumber > 0) BEGIN
		SELECT @orderId = cId FROM tblSynologenOrder WHERE cInvoiceNumber = @invoiceNumber
	END

	INSERT INTO tblSynologenOrderHistory (cOrderId, cText, cCreatedDate)
	VALUES (@orderId, @message, GETDATE())
	SELECT @id = @@IDENTITY	 

	SELECT @status = @@ERROR
	IF (@@ERROR <> 0) BEGIN
		SELECT @id = 0
		ROLLBACK TRANSACTION ADD_SYNOLOGEN_ORDERHISTORY
		RETURN
	END
								
	IF (@@ERROR = 0) BEGIN
		COMMIT TRANSACTION ADD_SYNOLOGEN_ORDERHISTORY
	END
