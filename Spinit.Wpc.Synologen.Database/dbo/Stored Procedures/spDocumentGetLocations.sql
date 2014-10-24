create PROCEDURE spDocumentGetLocations
					@type INT,
					@nodeId INT=-1,
					@status INT OUTPUT
	AS
		IF (@type = 1)
		BEGIN
			SELECT * FROM tblBaseLocations		
		END		
		IF (@type = 3)
		BEGIN
			SELECT * FROM tblDocumentNodeLocationConnection 
			INNER JOIN tblBaseLocations 
			ON tblDocumentNodeLocationConnection.cLocationId=tblBaseLocations.cId
				WHERE tblDocumentNodeLocationConnection.cDocumentNodeId = @nodeId		
		END
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
