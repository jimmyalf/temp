create PROCEDURE spDocumentConnectDisconnectNodeOrDocument
		@action INT=-1, 	--Connect = 0 / Disconnect = 1
		@type INT=-1, 		--Language = 0 / Location = 1 / File = 2
		@nodeId INT=0,
		@documentId INT=0,
		@languageId INT=0,
		@locationId INT=0,
		@fileId INT=0,
		@status INT OUTPUT				
AS
BEGIN
	IF (@action = 0)		--Connect
		BEGIN 
			
			IF (@type = 0)	-- Language
				BEGIN 
					INSERT INTO dbo.tblDocumentNodeLanguageConnection (cDocumentNodeId, cLanguageId)
					VALUES (@nodeId, @languageId)
				END	
			
			IF (@type = 1)	-- Location
				BEGIN 
					INSERT INTO dbo.tblDocumentNodeLocationConnection (cDocumentNodeId, cLocationId)
					VALUES (@nodeId, @locationId)
				END
			
			IF (@type = 2)	-- File
				BEGIN
					IF @nodeId IS NOT NULL
						BEGIN
							INSERT INTO dbo.tblDocumentNodeFileConnection (cDocumentNodeId, cFileId)
							VALUES (@nodeId, @fileId)							
						END
					
					IF @documentId IS NOT NULL
						BEGIN
							INSERT INTO dbo.tblDocumentDocumentFileConnection (cDocumentId, cFileId)
							VALUES (@documentId, @fileId)							
						END
				END
		END
	ELSE IF (@action = 1)	--Disconnect
		BEGIN 		
			IF (@type = 0)	-- Language
				BEGIN 
					DELETE FROM	dbo.tblDocumentNodeLanguageConnection
					WHERE		cDocumentNodeId = @nodeId 
						AND		cLanguageId = @languageId
				END	
			
			IF (@type = 1)	-- Location
				BEGIN 
					DELETE FROM	dbo.tblDocumentNodeLocationConnection
					WHERE		cDocumentNodeId = @nodeId
						AND		cLocationId = @locationId
				END	
			
			IF (@type = 2)	-- File
				BEGIN
					IF @nodeId IS NOT NULL
						BEGIN
							DELETE FROM	dbo.tblDocumentNodeFileConnection
							WHERE		cDocumentNodeId = @nodeId 
								AND		cFileId = @fileId							
						END
						
					IF @documentId IS NOT NULL
						BEGIN
							DELETE FROM	dbo.tblDocumentDocumentFileConnection
							WHERE		cDocumentId = @documentId
								AND		cFileId = @fileId
						END
				END	
		END
	
	IF (@@ERROR = 0) 
		BEGIN
			SELECT @status = 0
		END
	ELSE 
		BEGIN
			SELECT @status = @@ERROR
		END
END
