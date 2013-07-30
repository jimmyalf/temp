CREATE FUNCTION sfDocumentGetDocumentFiles
( )
RETURNS 
    @retTbl TABLE (
        [FleId]          INT             NULL,
        [ObjId]          INT             NULL,
        [ObjName]        NVARCHAR (50)   NULL,
        [ObjDescription] NVARCHAR (4000) NULL,
        [Component]      NVARCHAR (50)   NULL)
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	dbo.tblBaseFile.cId AS FleId,
					dbo.tblDocumentNode.cId AS ObjId,
					dbo.[sfBaseTruncateString] (SUBSTRING (dbo.tblDocumentNode.cName, 0, 254), 50) AS ObjName,
					NULL AS ObjDescription,
					'Document' AS Component
			FROM	dbo.tblBaseFile
				INNER JOIN	dbo.tblDocumentNodeFileConnection
					ON dbo.tblBaseFile.cId = dbo.tblDocumentNodeFileConnection.cFileId
				INNER JOIN	dbo.tblDocumentNode
					ON dbo.tblDocumentNodeFileConnection.cDocumentNodeId = dbo.tblDocumentNode.cId
		UNION
			SELECT	dbo.tblBaseFile.cId AS FleId,
					dbo.tblDocuments.cId AS ObjId,
					dbo.[sfBaseTruncateString] (SUBSTRING (dbo.tblDocuments.cName, 0, 254), 50) AS ObjName,
					NULL AS ObjDescription,
					'Document' AS Component
			FROM	dbo.tblBaseFile
				INNER JOIN	dbo.tblDocumentDocumentFileConnection
					ON dbo.tblBaseFile.cId = dbo.tblDocumentDocumentFileConnection.cFileId
				INNER JOIN	dbo.tblDocuments
					ON dbo.tblDocumentDocumentFileConnection.cDocumentId = dbo.tblDocuments.cId
							
	RETURN
END
