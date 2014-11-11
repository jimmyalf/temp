create PROCEDURE spSynologenUpdateShopCategoryMemberCategoryConnection
		@type INT,
		@shopCategoryId INT,
		@memberCategoryId INT,
		@status INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SCMCC

		IF (@type = 1) BEGIN --Connect
			INSERT INTO tblSynologenShopCategoryMemberCategoryConnection
				(cShopCategoryId, cMemberCategoryId)
			VALUES
				(@shopCategoryId, @memberCategoryId)
		END			 
		IF (@type = 2) BEGIN --Disconnect
			DELETE FROM tblSynologenShopCategoryMemberCategoryConnection
			WHERE cShopCategoryId = @shopCategoryId
			OR	cMemberCategoryId = @memberCategoryId					
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SCMCC
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SCMCC
			END
