create FUNCTION sfContGetAllPageLinks
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
        [LngId]          INT             NULL,
        [TemplateId]     INT             NULL,
        [StylesheetId]   INT             NULL)
AS
BEGIN
	INSERT INTO @retTbl
	SELECT 
		lnk.cId AS LnkId,
		pge.cId AS ObjId,
		dbo.[sfBaseTruncateString](pge.cName,50) AS ObjName,
		dbo.sfContGetTreDownString (lnk.cId, NULL) AS ObjDescription,
		'Content' AS Component,
		'Page' AS ObjectType,
		lnk.cLocId AS LocId,
		lnk.cLngId AS LngId,
		lnk.cTemplate AS TemplateId,
		lnk.cStyleSheet AS StyleSheetId
	FROM tblContTree lnk INNER JOIN	tblContPage pge ON lnk.cPgeId = pge.cId
	WHERE pge.cPgeTpeId = 1 AND NOT lnk.cTreTpeId = 16 
RETURN
END
