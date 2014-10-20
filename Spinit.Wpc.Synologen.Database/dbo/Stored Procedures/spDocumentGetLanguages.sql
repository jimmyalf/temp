CREATE PROCEDURE spDocumentGetLanguages
					@type INT,
					@nodeId INT=-1,
					@status INT OUTPUT
	AS
		IF (@type = 1)
		BEGIN
			SELECT * FROM tblBaseLanguages		
		END		
		IF (@type = 3)
		BEGIN
			SELECT * FROM tblDocumentNodeLanguageConnection 
			INNER JOIN tblBaseLanguages
			ON tblDocumentNodeLanguageConnection.cLanguageId=tblBaseLanguages.cId
				WHERE tblDocumentNodeLanguageConnection.cDocumentNodeId = @nodeId		
		END
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
