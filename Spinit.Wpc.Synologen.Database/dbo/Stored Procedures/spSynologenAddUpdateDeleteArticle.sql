CREATE PROCEDURE spSynologenAddUpdateDeleteArticle
		@type INT,
		@number NVARCHAR(50) = '',
		@name NVARCHAR(50) = NULL,
		@description NVARCHAR(255) = NULL,
		@spcsAccountNumber NVARCHAR(25) = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_UPDATE_DELETE_ARTICLE

		IF (@type = 0) BEGIN --Create
			INSERT INTO tblSynologenArticle (cArticleNumber, cName, cDescription, cDefaultSPCSAccountNumber)
			VALUES (@number, @name, @description, @spcsAccountNumber)
			SELECT @id = @@IDENTITY
		END			 

		IF (@type = 1) BEGIN --Update
			UPDATE tblSynologenArticle
				SET 
				cArticleNumber = @number,
				cName = @name,
				cDescription = @description,
				cDefaultSPCSAccountNumber = @spcsAccountNumber
			WHERE cId = @id
			
		END
		IF (@type = 2) BEGIN

			DELETE FROM tblSynologenContractArticleConnection
			WHERE cArticleId = @id		

			DELETE FROM tblSynologenArticle
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_ARTICLE
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_ARTICLE
			END
