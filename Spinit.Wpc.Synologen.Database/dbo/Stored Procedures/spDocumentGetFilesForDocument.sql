create PROCEDURE spDocumentGetFilesForDocument
	@type INT,	-- All = 1, Connected = 3
	@documentId INT=-1,
	@status INT OUTPUT
AS
BEGIN
	IF (@type = 1)
		BEGIN
			SELECT	* 
			FROM dbo.tblDocumentDocumentFileConnection		
		END
		
	IF (@type = 3)
		BEGIN
			SELECT	* 
			FROM	dbo.tblDocumentDocumentFileConnection 
			WHERE	dbo.tblDocumentDocumentFileConnection.cDocumentId = @documentId	
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
