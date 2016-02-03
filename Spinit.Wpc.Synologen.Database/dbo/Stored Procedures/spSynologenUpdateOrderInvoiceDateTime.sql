-- =============================================
-- Author:		CBER
-- Create date: 2011-09-16
-- Description:	Sets invoice date time for a given order
-- =============================================
create PROCEDURE spSynologenUpdateOrderInvoiceDateTime
	@orderId int, 
	@invoiceDateTime datetime,
	@status int output
AS
BEGIN
	UPDATE tblSynologenOrder 
		SET cInvoiceDate = @invoiceDateTime 
	WHERE tblSynologenOrder.cId = @orderId
	
	SELECT @status = @@ERROR
END
