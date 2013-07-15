CREATE PROCEDURE spSynologenGetContractsDynamic
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
					
AS
BEGIN
	DECLARE @sql nvarchar(4000),
			@paramlist nvarchar(4000)
	SELECT @sql=
	'SELECT 
		cId,
		cCode,
		cName,
		cDescription,
		cAddress,
		cAddress2,
		cZip,
		cCity,
		cPhone,
		cPhone2,
		cFax,
		cEmail,
		cActive,
		(SELECT COUNT (cId) FROM tblSynologenCompany WHERE cContractCustomerId = tblSynologenContract.cId) AS cNumberOfCompanies,
		(SELECT COUNT (cId) FROM tblSynologenContractArticleConnection WHERE cContractCustomerId = tblSynologenContract.cId) AS cNumberOfArticles
		FROM tblSynologenContract'

		SELECT @sql = @sql + ' WHERE 1=1'

		IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
		BEGIN
			SELECT @sql = @sql + 
			' AND (cId LIKE ''%''+@xSearchString+
			''%''OR cName LIKE ''%''+@xSearchString+''%''+
			''%''OR cCode LIKE ''%''+@xSearchString+''%''+
			''%''OR cDescription LIKE ''%''+@xSearchString+''%''+
			''%''OR cEmail LIKE ''%''+@xSearchString+''%''+
			''%''OR cAddress LIKE ''%''+@xSearchString+''%''+
			''%''OR cAddress2 LIKE ''%''+@xSearchString+''%''+
			''%''OR cZip LIKE ''%''+@xSearchString+''%''+
			''%''OR cPhone LIKE ''%''+@xSearchString+''%''+
			''%''OR cPhone2 LIKE ''%''+@xSearchString+''%''+
			''%''OR cCity LIKE ''%''+@xSearchString+''%'')'
		END
		IF (@OrderBy IS NOT NULL AND LEN(@OrderBy) > 0)
		BEGIN
			SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
		END
		SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255)'
		EXEC sp_executesql @sql,
							@paramlist,                                 
							@SearchString,
							@OrderBy
		

END
