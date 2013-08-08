create PROCEDURE spSynologenGetShopCategoryMemberCategoryConnections
					@shopCategoryId INT,					
					@memberCategoryId INT,
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenShopCategoryMemberCategoryConnection.*
		FROM tblSynologenShopCategoryMemberCategoryConnection'

	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@shopCategoryId IS NOT NULL AND @shopCategoryId > 0)
	BEGIN
		SELECT @sql = @sql + 
			' AND tblSynologenShopCategoryMemberCategoryConnection.cShopCategoryId = @xShopCategoryId'
	END
	IF (@memberCategoryId IS NOT NULL AND @memberCategoryId > 0)
	BEGIN
		SELECT @sql = @sql + 
			' AND tblSynologenShopCategoryMemberCategoryConnection.cMemberCategoryId = @xMemberCategoryId'
	END

	SELECT @paramlist = '@xShopCategoryId INT, @xMemberCategoryId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@shopCategoryId,
						@memberCategoryId
				

END
