create PROCEDURE spSynologenGetShopEquipment
					@equipmentId INT,
					@shopId INT,
					@orderBy NVARCHAR(50),
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT	tblSynologenShopEquipment.*
		FROM tblSynologenShopEquipment'

	IF (@shopId IS NOT NULL AND @shopId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenShopEquipmentConnection ON tblSynologenShopEquipmentConnection.cShopEquipmentId = tblSynologenShopEquipment.cId
		AND tblSynologenShopEquipmentConnection.cShopId = @xShopId'
	END
		
	SELECT @sql = @sql + ' WHERE 1=1'

	IF (@equipmentId IS NOT NULL AND @equipmentId > 0) BEGIN
		SELECT @sql = @sql + ' AND tblSynologenShopEquipment.cId = @xId'
	END
	IF (@orderBy IS NOT NULL) BEGIN
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
	END
	SELECT @paramlist = '@xId INT,@xShopId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@equipmentId,
						@shopId
				

END
