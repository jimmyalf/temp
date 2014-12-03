create FUNCTION sfNewsGetNewsFiles
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
			SELECT	tblBaseFile.cId AS FleId,
					tblNews.cId AS ObjId,
					--tblNews.cHeading AS ObjName,
					dbo.[sfBaseTruncateString](SUBSTRING(tblNews.cHeading,0,254),50) AS ObjName,
					NULL AS ObjDescription,
					'News' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblNewsFileConnection
					ON tblBaseFile.cId = tblNewsFileConnection.cFileId
				INNER JOIN	tblNews
					ON tblNewsFileConnection.cNewsId = tblNews.cId					
	RETURN
END
