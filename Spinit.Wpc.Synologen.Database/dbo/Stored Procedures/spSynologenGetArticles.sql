create PROCEDURE spSynologenGetArticles
					@articleId INT,					
					@contractCustomerId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenArticle.*
		FROM tblSynologenArticle'

	IF (@contractCustomerId IS NOT NULL AND @contractCustomerId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenContractArticleConnection ON tblSynologenContractArticleConnection.cArticleId = tblSynologenArticle.cId
		AND tblSynologenContractArticleConnection.cContractCustomerId = @xContractCustomerId'
	END
	IF (@articleId IS NOT NULL AND @articleId > 0)
	BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenArticle.cId = @xArticleId'
	END


	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xArticleId INT, @xContractCustomerId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@articleId,
						@contractCustomerId
				

END
