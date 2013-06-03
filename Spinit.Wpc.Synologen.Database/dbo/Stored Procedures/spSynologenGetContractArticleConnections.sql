CREATE PROCEDURE spSynologenGetContractArticleConnections
					@connectionId INT,					
					@contractCustomerId INT,
					@orderBy NVARCHAR(255),
					@active BIT,
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql= 
		'SELECT	
			tblSynologenContractArticleConnection.*,
			tblSynologenArticle.cName,
			tblSynologenArticle.cArticleNumber,
			tblSynologenArticle.cDescription,
			(SELECT cName FROM tblSynologenContract WHERE tblSynologenContract.cId = tblSynologenContractArticleConnection.cContractCustomerId) AS cContractName
		FROM tblSynologenContractArticleConnection
		INNER JOIN tblSynologenArticle ON tblSynologenArticle.cId = tblSynologenContractArticleConnection.cArticleId'

	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@contractCustomerId IS NOT NULL AND @contractCustomerId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenContractArticleConnection.cContractCustomerId = @xContractCustomerId'
	END
	
	IF (@connectionId IS NOT NULL AND @connectionId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenContractArticleConnection.cId = @xConnectionId'
	END
	
	IF (@active IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenContractArticleConnection.cActive = @xActive'
	END

	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	
	SELECT @paramlist = '@xConnectionId INT, @xContractCustomerId INT, @xActive BIT'
	EXEC sp_executesql @sql,
						@paramlist,
						@connectionId,
						@contractCustomerId,
						@active
				

END
