create FUNCTION sfContGetPageLinks
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
					tblContPage.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblContPage.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Content' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblContPageFile
					ON tblBaseFile.cId = tblContPageFile.cFleId
				INNER JOIN	tblContPage
					ON tblContPageFile.cPgeId = tblContPage.cId
			WHERE	tblContPage.cId NOT IN (SELECT cPgeId FROM tblContTree)
		UNION
			SELECT	tblBaseFile.cId AS FleId,
					tblContTree.cPgeId AS ObjId,
					--tblContTree.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblContTree.cName,50) As ObjName, 
					dbo.sfContGetTreDownString (tblContTree.cId, NULL) 
						AS ObjDescription,
					'Content' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblContPageFile
					ON tblBaseFile.cId = tblContPageFile.cFleId
				INNER JOIN	tblContTree
					ON tblContPageFile.cPgeId = tblContTree.cPgeId
					
	RETURN
END
