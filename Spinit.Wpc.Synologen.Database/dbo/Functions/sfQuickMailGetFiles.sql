create FUNCTION sfQuickMailGetFiles
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
					tblQuickMailMail.cId AS ObjId,
					dbo.[sfBaseTruncateString](tblQuickMailMail.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'QuickMail' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblQuickMailFileConnection
					ON tblQuickMailFileConnection.cFleId = tblBaseFile.cId
				INNER JOIN tblQuickMailMail
					ON tblQuickMailMail.cId = tblQuickMailFileConnection.cMlId
					
	RETURN
END
