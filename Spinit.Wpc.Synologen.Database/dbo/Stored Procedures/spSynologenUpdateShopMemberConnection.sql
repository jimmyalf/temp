create PROCEDURE spSynologenUpdateShopMemberConnection
		@type INT,
		@shopId INT,
		@memberId INT,
		@status INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SHOPCONN

		IF (@type = 1) BEGIN --Connect
			INSERT INTO tblSynologenShopMemberConnection
				(cSynologenShopId,	cMemberId)
			VALUES
				(@shopId, @memberId)
		END			 
		IF (@type = 2) BEGIN --Disconnect
			DELETE FROM tblSynologenShopMemberConnection
			WHERE cMemberId = @memberId								
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_SHOPCONN
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_SHOPCONN
			END
