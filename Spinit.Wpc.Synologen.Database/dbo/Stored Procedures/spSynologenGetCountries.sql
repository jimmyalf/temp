create PROCEDURE spSynologenGetCountries
					@countryId INT,					
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenCountry.*
		FROM tblSynologenCountry'

	IF (@countryId IS NOT NULL AND @countryId > 0)
	BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenCountry.cId = @xCountryId'
	END
	
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xCountryId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@countryId
				

END
