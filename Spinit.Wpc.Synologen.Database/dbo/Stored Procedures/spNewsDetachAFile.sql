create PROCEDURE spNewsDetachAFile
					@newsId INT,
					@fileId INT,
					@status INT OUTPUT
	AS

		BEGIN
			DELETE FROM tblNewsAttachments
			WHERE cNewsId = @newsId AND cFileId = @fileId
		END
				
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
