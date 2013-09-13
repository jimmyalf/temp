create PROCEDURE spSynologenGetShopCategories
					@categoryId INT,
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	tblSynologenShopCategory.*
		FROM tblSynologenShopCategory'

		
	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@categoryId IS NOT NULL AND @categoryId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShopCategory.cId = @xCategoryId'
	END
	SELECT @paramlist = '@xCategoryId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@categoryId
				

END
