create PROCEDURE spSynologenGetConcerns
					@concernId INT,					
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenConcern.*
		FROM tblSynologenConcern'

	IF (@concernId IS NOT NULL AND @concernId > 0)
	BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenConcern.cId = @xConcernId'
	END
	
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xConcernId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@concernId
				

END
