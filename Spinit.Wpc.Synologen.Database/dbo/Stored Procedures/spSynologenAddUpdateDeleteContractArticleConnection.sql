CREATE PROCEDURE spSynologenAddUpdateDeleteContractArticleConnection
		@type INT,
		@contractCustomerId INT = 0,
		@articleId INT = 0,
		@price FLOAT = 0,
		@noVAT BIT = 0,
		@active BIT = 0,
		@SPCSAccountNumber NVARCHAR(50) = '',
		@enableManualPriceOverride BIT = 0,
		@status INT OUTPUT,
		@id INT OUTPUT



	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_ARTICLECONN

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenContractArticleConnection 
				(cContractCustomerId, cArticleId, cPrice, cActive, cNoVAT, cSPCSAccountNumber, cEnableManualPriceOverride)
			VALUES 
				(@contractCustomerId, @articleId, @price, @active, @noVAT, @SPCSAccountNumber, @enableManualPriceOverride)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenContractArticleConnection
				SET 
				cContractCustomerId = @contractCustomerId,
				cArticleId = @articleId,
				cPrice = @price,	
				cActive = @active,
				cNoVAT = @noVAT,
				cSPCSAccountNumber = @SPCSAccountNumber,
				cEnableManualPriceOverride = @enableManualPriceOverride
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN

			DELETE FROM tblSynologenContractArticleConnection
			WHERE cId = @id		

		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_ARTICLECONN
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_ARTICLECONN
			END
