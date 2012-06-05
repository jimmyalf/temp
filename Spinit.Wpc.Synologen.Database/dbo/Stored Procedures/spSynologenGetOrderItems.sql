CREATE PROCEDURE spSynologenGetOrderItems
					@orderId INT,
					@articleId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenOrderItems.*,
			tblSynologenArticle.cName As cArticleName,
			tblSynologenArticle.cArticleNumber
		FROM tblSynologenOrderItems
		INNER JOIN tblSynologenArticle ON tblSynologenArticle.cId = tblSynologenOrderItems.cArticleId'

		
	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@orderId IS NOT NULL AND @orderId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrderItems.cOrderId = @xOrderId'
	END
	IF (@articleId IS NOT NULL AND @articleId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenOrderItems.cArticleId = @xArticleId'
	END
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xOrderId INT, @xArticleId INT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@orderId,
						@articleId

END
