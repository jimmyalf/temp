create FUNCTION sfNewsGetNewsPages
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
					tblNews.cId AS ObjId,
					--tblNews.cHeading AS ObjName,
					dbo.[sfBaseTruncateString](SUBSTRING(tblNews.cHeading,0,254),50) As ObjName,
					NULL AS ObjDescription,
					'News' AS Component,
					'News' AS ObjectType,
					tblNewsLocationConnection.cLocationId AS LocId,
					tblNewsLanguageConnection.cLanguageId AS LngId
			FROM tblContTree	
				INNER JOIN tblContPage
					ON tblContTree.cPgeId = tblContPage.cId
				INNER JOIN	tblNewsPageConnection
					ON tblContPage.cId = tblNewsPageConnection.cPageId
				INNER JOIN	tblNews
					ON tblNewsPageConnection.cNewsId = tblNews.cId
				LEFT OUTER JOIN tblNewsLocationConnection
					ON tblNewsLocationConnection.cNewsId = tblNews.cId			
				LEFT OUTER JOIN tblNewsLanguageConnection
					ON tblNewsLanguageConnection.cNewsId = tblNews.cId			
	RETURN
END
