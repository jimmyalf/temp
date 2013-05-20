CREATE PROCEDURE spSynologenAddLog
		@type INT,
		@message NVARCHAR(2000) = NULL,
		@status INT OUTPUT,
		@id INT OUTPUT


	AS BEGIN TRANSACTION ADD_SYNOLOGEN_LOG

		INSERT INTO tblSynologenLog (cMessage, cMessageType, cCreatedDate)
		VALUES (@message, @type, GETDATE())
		SELECT @id = @@IDENTITY	 

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_SYNOLOGEN_LOG
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_SYNOLOGEN_LOG
			END
