create PROCEDURE spSynologenGetGiros
					@giroId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenGiro.*
		FROM tblSynologenGiro'

	IF (@giroId IS NOT NULL AND @giroId > 0) BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenGiro.cId = @xGiroId'
	END

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xGiroId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@giroId
				

END
