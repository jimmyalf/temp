CREATE PROCEDURE spSynologenGetContracts
	@type INT,
	@contractCustomerId INT = 0,
	@shopId INT = 0,
	@active BIT = 0,
	@status INT OUTPUT
AS
	BEGIN

		IF (@type = 1) BEGIN --All
			IF(@active IS NOT NULL) BEGIN
				SELECT * FROM tblSynologenContract 
				WHERE tblSynologenContract.cActive = @active
			END
			ELSE BEGIN
				SELECT * FROM tblSynologenContract
			END
			
		END

		IF (@type = 2) BEGIN --All per shop
			IF(@active IS NOT NULL) BEGIN
				SELECT	* FROM tblSynologenContract
				INNER JOIN tblSynologenShopContractConnection 
					ON tblSynologenShopContractConnection.cSynologenContractCustomerId = tblSynologenContract.cId
				WHERE tblSynologenShopContractConnection.cSynologenShopId = @shopId
				AND tblSynologenContract.cActive = @active
			END
			ELSE BEGIN
				SELECT	* FROM tblSynologenContract
				INNER JOIN tblSynologenShopContractConnection 
					ON tblSynologenShopContractConnection.cSynologenContractCustomerId = tblSynologenContract.cId
				WHERE tblSynologenShopContractConnection.cSynologenShopId = @shopId
			END
			--TODO : implement functionality here
		END		

		IF (@type = 3) BEGIN --Specific
			IF(@active IS NOT NULL) BEGIN
				SELECT	* FROM tblSynologenContract
				WHERE cId = @contractCustomerId
				AND tblSynologenContract.cActive = @active
			END
			ELSE BEGIN
				SELECT	* FROM tblSynologenContract
				WHERE cId = @contractCustomerId
			END
		END

		SELECT @status = @@ERROR
	END
