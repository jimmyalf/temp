create PROCEDURE spNewsAttachAFile
					@newsId INT,
					@fileId INT,
					@status INT OUTPUT				
	AS
	BEGIN	
		INSERT INTO tblNewsAttachments
			(cFileId, cNewsId)
		VALUES
			(@fileId, @newsId)		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
