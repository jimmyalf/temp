CREATE FUNCTION sfSynologenGetShopEquipmentString (@shopId INT)
	RETURNS NVARCHAR (4000)
	AS BEGIN

		DECLARE @first INT
		SET @first=1
		DECLARE @tmpString NVARCHAR(4000)
		DECLARE @retString NVARCHAR(4000)

		DECLARE get_string CURSOR LOCAL FOR
			SELECT	cName
			FROM	tblSynologenShopEquipment
			INNER JOIN tblSynologenShopEquipmentConnection ON tblSynologenShopEquipmentConnection.cShopEquipmentId = tblSynologenShopEquipment.cId
			WHERE tblSynologenShopEquipmentConnection.cShopId = @shopId
			
		OPEN get_string
		FETCH NEXT FROM get_string INTO @tmpString
		
		WHILE (@@FETCH_STATUS <> -1)
			BEGIN
				IF (@first = 1)
					BEGIN
						SELECT @retString = @tmpString
						SELECT @first = 0
					END
				ELSE
					BEGIN
						SELECT @retString = @tmpString + ', ' + @retString
					END
				
				FETCH NEXT FROM get_string INTO @tmpString
			END
			
		CLOSE get_string
		DEALLOCATE get_string
		
		RETURN (@retString)
	END
