CREATE PROCEDURE spSynologenUpdateShopContractConnection
		@type INT,
		@shopId INT,
		@contractCustomerId INT,
		@status INT OUTPUT

	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_SHOPCONN

		IF (@type = 1) BEGIN --Connect
			INSERT INTO tblSynologenShopContractConnection
				(cSynologenShopId,	cSynologenContractCustomerId)
			VALUES
				(@shopId, @contractCustomerId)
		END			 
		IF (@type = 2) BEGIN --Disconnect
			IF ((@shopId IS NOT NULL) AND (@shopId > 0) AND (@contractCustomerId IS NOT NULL) AND (@contractCustomerId > 0)) BEGIN
				DELETE FROM tblSynologenShopContractConnection
				WHERE cSynologenShopId = @shopId AND cSynologenContractCustomerId = @contractCustomerId				
			END		
			ELSE IF (@shopId IS NOT NULL AND @shopId > 0) BEGIN
				DELETE FROM tblSynologenShopContractConnection
				WHERE cSynologenShopId = @shopId			
			END
			ELSE IF (@contractCustomerId IS NOT NULL AND @contractCustomerId > 0) BEGIN
				DELETE FROM tblSynologenShopContractConnection
				WHERE cSynologenContractCustomerId = @contractCustomerId			
			END							
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
