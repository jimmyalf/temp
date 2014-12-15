CREATE PROCEDURE spSynologenGetShops
					@shopId INT,
					@shopCategoryId INT,
					@contractCustomerId INT,
					@memberId INT,
					@equipmentId INT,
					@includeInactive BIT,
					@concernId INT,
					@orderBy NVARCHAR(255),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	
			tblSynologenShop.*,
			(CASE WHEN cShopNumber IS NOT NULL THEN (cShopName + '' (''+cShopNumber+'')'') ELSE cShopName END) AS cDetailName,
			dbo.sfSynologenGetShopEquipmentString(tblSynologenShop.cId) AS cEquipment
		FROM tblSynologenShop'

	IF (@memberId IS NOT NULL AND @memberId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenShopMemberConnection ON tblSynologenShopMemberConnection.cSynologenShopId = tblSynologenShop.cId
		AND tblSynologenShopMemberConnection.cMemberId = @xMemberId'
	END
	IF (@contractCustomerId IS NOT NULL AND @contractCustomerId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenShopContractConnection ON tblSynologenShopContractConnection.cSynologenShopId = tblSynologenShop.cId
		AND tblSynologenShopContractConnection.cSynologenContractCustomerId = @xContractCustomerId'
	END
	IF (@equipmentId IS NOT NULL AND @equipmentId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenShopEquipmentConnection ON tblSynologenShopEquipmentConnection.cShopId = tblSynologenShop.cId
		AND tblSynologenShopEquipmentConnection.cShopEquipmentId = @xEquipmentId'
	END
		
	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShop.cId = @xShopId'
	END
	IF (@shopCategoryId IS NOT NULL AND @shopCategoryId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShop.cCategoryId = @xCategoryId'
	END
	IF(@includeInactive <> 1) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShop.cActive = 1'
	END
	IF(@concernId <> 1) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShop.cConcernId = @xConcernId'
	END	
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xMemberId INT, @xShopId INT, @xCategoryId INT, @xContractCustomerId INT, @xEquipmentId INT, @xConcernId INT'
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@memberId,
						@shopId,
						@shopCategoryId,
						@contractCustomerId,
						@equipmentId,
						@concernId
				

END
