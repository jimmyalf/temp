CREATE PROCEDURE spSynologenUpdateShopEquipmentConnection
		@type INT,
		@equipmentId INT,
		@shopId INT,
		@notes NVARCHAR(255) = NULL,
		@status INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SEC

		IF (@type = 1) BEGIN --Connect
			INSERT INTO tblSynologenShopEquipmentConnection
				(cShopEquipmentId,	cShopId, cNotes)
			VALUES
				(@equipmentId, @shopId, @notes)
		END			 
		IF (@type = 2) BEGIN --Disconnect
			IF (@equipmentId > 0 AND @shopId > 0) BEGIN
				DELETE FROM tblSynologenShopEquipmentConnection
				WHERE cShopEquipmentId = @equipmentId								
				AND cShopId = @shopId
			END
			ELSE IF (@equipmentId > 0) BEGIN
				DELETE FROM tblSynologenShopEquipmentConnection
				WHERE cShopEquipmentId = @equipmentId								
			END
			ELSE IF (@shopId > 0) BEGIN
				DELETE FROM tblSynologenShopEquipmentConnection
				WHERE cShopId = @shopId						
			END
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SEC
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SEC
			END
