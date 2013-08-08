create PROCEDURE spSynologenGetInvoicingMethods
					@invoicingMethodId INT,					
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenInvoiceMethod.*
		FROM tblSynologenInvoiceMethod'

	IF (@invoicingMethodId IS NOT NULL AND @invoicingMethodId > 0) 	BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenInvoiceMethod.cId = @xInvoicingMethodId'
	END


	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	
	SELECT @paramlist = '@xInvoicingMethodId INT'
	
	EXEC sp_executesql @sql,
						@paramlist,
						@invoicingMethodId
						
	SELECT @status = @@ERROR
				

END
