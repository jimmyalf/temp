CREATE PROCEDURE spSynologenAddUpdateDeleteOrderItem
		@type INT,
		@orderId INT = 0,
		@articleId INT = 0,
		@singlePrice FLOAT = 0,
		@numberOfItems INT = 0,
		@notes NVARCHAR(255) = NULL,
		@noVAT BIT = 0,
		@SPCSAccountNumber NVARCHAR(50)='',
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_OI

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenOrderItems
				(cOrderId, cArticleId, cSinglePrice, cNumberOfItems, cNotes,
				cNoVAT,
				cSPCSAccountNumber
				)
			VALUES
				(@orderId, @articleId, @singlePrice, @numberOfItems,@notes,
				dbo.sfSynologenGetArticleVATForOrderItem(@orderId,@articleId),
				dbo.sfSynologenGetArticleSPCSAccountNumberForOrderItem(@orderId,@articleId)
				)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenOrderItems
				SET 
				cOrderId = @orderId,
				cArticleId = @articleId,
				cSinglePrice = @singlePrice,
				cNumberOfItems = @numberOfItems,
				cNotes = @notes,
				cNoVAT = @noVAT,
				cSPCSAccountNumber = @SPCSAccountNumber
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN
			DELETE FROM tblSynologenOrderItems			
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_OI
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_OI
			END
