create PROCEDURE spNewsGetAttachments
					@type INT,
					@newsId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@TYPE = 0)
			BEGIN
				SELECT * 
				FROM tblBaseFile
				INNER JOIN tblNewsAttachments
				ON tblBaseFile.cId=tblNewsAttachments.cFileId
				WHERE tblNewsAttachments.cNewsId = @newsId
			END
			
			
			SELECT @status = @@ERROR
		END
