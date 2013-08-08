CREATE PROCEDURE spSynologenGetCompanyRSTs
					@rstId INT,					
					@companyId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT

					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)

	SELECT @sql= 'SELECT 
					tblSynologenRst.*,
					(SELECT COUNT (*) FROM tblSynologenOrder WHERE tblSynologenOrder.cRstId = tblSynologenRst.cId)AS cConnectedOrders
				 FROM tblSynologenRst WHERE 1=1'

	IF (@rstId IS NOT NULL AND @rstId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenRst.cId = @xRstId'
	END

	IF (@companyId IS NOT NULL AND @companyId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenRst.cCompanyId = @xCompanyId'
	END

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END

	SELECT @paramlist = '@xRstId INT, @xCompanyId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@rstId,
						@companyId
				

END
