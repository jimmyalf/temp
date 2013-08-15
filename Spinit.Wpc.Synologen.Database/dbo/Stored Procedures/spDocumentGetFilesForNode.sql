create PROCEDURE spDocumentGetFilesForNode
	@type INT,	-- All = 1, Connected = 3
	@nodeId INT=-1,
	@status INT OUTPUT
AS
BEGIN
	IF (@type = 1)
		BEGIN
			SELECT	* 
			FROM dbo.tblDocumentNodeFileConnection		
		END
		
	IF (@type = 3)
		BEGIN
			SELECT	* 
			FROM	dbo.tblDocumentNodeFileConnection 
			WHERE	dbo.tblDocumentNodeFileConnection.cDocumentNodeId = @nodeId		
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
