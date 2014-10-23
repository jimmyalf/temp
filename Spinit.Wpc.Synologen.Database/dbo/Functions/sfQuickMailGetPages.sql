create FUNCTION sfQuickMailGetPages
( )
RETURNS 
    @retTbl TABLE (
        [LnkId]          INT             NULL,
        [ObjId]          INT             NULL,
        [ObjName]        NVARCHAR (50)   NULL,
        [ObjDescription] NVARCHAR (4000) NULL,
        [Component]      NVARCHAR (50)   NULL,
        [ObjectType]     NVARCHAR (50)   NULL,
        [LocId]          INT             NULL,
        [LngId]          INT             NULL)
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	tblContTree.cId AS LnkId,
					tblQuickMailMail.cId AS ObjId,
					dbo.[sfBaseTruncateString](tblQuickMailMail.cName,50) As ObjName,
					NULL AS ObjDescription,
					'QuickMail' AS Component,
					'QuickMail' AS ObjectType,
					tblQuickMailLocationConnection.cLocId AS LocId,
					tblQuickMailLanguageConnection.cLngId AS LngId
			FROM tblContTree	
				INNER JOIN tblContPage
					ON tblContTree.cPgeId = tblContPage.cId
				INNER JOIN	tblQuickMailPageConnection
					ON tblContPage.cId = tblQuickMailPageConnection.cPgeId
				INNER JOIN tblQuickMailMail
					ON tblQuickMailMail.cId = tblQuickMailPageConnection.cMlId
				LEFT OUTER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId			
				LEFT OUTER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId			
					
	RETURN
END
