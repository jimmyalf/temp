CREATE PROCEDURE spSynologenGetShopsDynamic
					@categoryId INT,
					@contractCustomerId INT,
					@equipmentId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
					
AS
BEGIN
	DECLARE @sql nvarchar(4000),
			@paramlist nvarchar(4000)
	SELECT @sql=
	'SELECT 
		tblSynologenShop.cId,
		tblSynologenShop.cShopName,
		tblSynologenShop.cShopNumber,
		tblSynologenShop.cShopDescription,
		tblSynologenShop.cContactFirstName,
		tblSynologenShop.cContactLastName,
		tblSynologenShop.cEmail,
		tblSynologenShop.cPhone,
		tblSynologenShop.cPhone2,
		tblSynologenShop.cFax,
		tblSynologenShop.cUrl,
		tblSynologenShop.cMapUrl,
		tblSynologenShop.cAddress,
		tblSynologenShop.cAddress2,
		tblSynologenShop.cZip,
		tblSynologenShop.cCity,
		tblSynologenShop.cActive,
		tblSynologenShop.cOrganizationNumber,
		(SELECT cName FROM tblSynologenShopCategory WHERE cId = tblSynologenShop.cCategoryId),
		(SELECT COUNT (*) FROM tblSynologenShopMemberConnection WHERE cSynologenShopId = tblSynologenShop.cId) As cNumberOfMembers
		FROM tblSynologenShop'

		IF (@contractCustomerId IS NOT NULL AND @contractCustomerId > 0) BEGIN
			SELECT @sql = @sql + ' INNER JOIN tblSynologenShopContractConnection ON tblSynologenShopContractConnection.cSynologenShopId = tblSynologenShop.cId'
			SELECT @sql = @sql + ' AND tblSynologenShopContractConnection.cSynologenContractCustomerId = @xContractCustomerId'
		END

		IF (@equipmentId IS NOT NULL AND @equipmentId > 0)
		BEGIN
			SELECT @sql = @sql + 
			' INNER JOIN tblSynologenShopEquipmentConnection ON tblSynologenShopEquipmentConnection.cShopId = tblSynologenShop.cId
			AND tblSynologenShopEquipmentConnection.cShopEquipmentId = @xEquipmentId'
		END

		SELECT @sql = @sql + ' WHERE 1=1'

		IF (@categoryId IS NOT NULL AND @categoryId > 0) BEGIN
			SELECT @sql = @sql + ' AND cCategoryId = @xCategoryId'
		END


		IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
		BEGIN
			SELECT @sql = @sql + 
			' AND (tblSynologenShop.cId LIKE ''%''+@xSearchString+
			''%''OR cOrganizationNumber LIKE ''%''+@xSearchString+''%''+
			''%''OR cShopName LIKE ''%''+@xSearchString+''%''+
			''%''OR cShopNumber LIKE ''%''+@xSearchString+''%''+
			''%''OR cShopDescription LIKE ''%''+@xSearchString+''%''+
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
		SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xContractCustomerId INT,@xCategoryId INT, @xEquipmentId INT'
		EXEC sp_executesql @sql,
							@paramlist,                                 
							@SearchString,
							@OrderBy,
							@contractCustomerId,
							@categoryId,
							@equipmentId

END
