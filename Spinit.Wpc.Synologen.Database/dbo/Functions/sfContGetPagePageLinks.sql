create FUNCTION sfContGetPagePageLinks
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
				SELECT	lnk.cId AS LnkId,
						pge.cId AS ObjId,
						--pge.cName AS ObjName,
						dbo.[sfBaseTruncateString](pge.cName,50) AS ObjName,
						NULL AS ObjDescription,
						'Content' AS Component,
						'Page' AS ObjectType,
						lnk.cLocId AS LocId,
						lnk.cLngId AS LngId
				FROM	tblContTree lnk
					INNER JOIN	tblContPagePage
						ON lnk.cPgeId = tblContPagePage.cLnkId
					INNER JOIN	tblContPage pge
						ON tblContPagePage.cPgeId = pge.cId
				WHERE	pge.cId NOT IN (SELECT cPgeId FROM tblContTree)
			UNION
				SELECT	lnk.cId AS LnkId,
						tre.cId AS ObjId,
						--tre.cName AS ObjName,
						dbo.[sfBaseTruncateString](tre.cName,50) AS ObjName,
						dbo.sfContGetTreDownString (tre.cId, NULL) 
							AS ObjDescription,
						'Content' AS Component,
						'Tree' AS ObjectType,
						lnk.cLocId AS LocId,
						lnk.cLngId AS LngId
				FROM	tblContTree lnk 
					INNER JOIN tblContPage
						ON tblContPage.cId = lnk.cPgeId
					INNER JOIN	tblContPagePage
						ON tblContPage.cId = tblContPagePage.cLnkId
					INNER JOIN	tblContTree tre
						ON tblContPagePage.cPgeId = tre.cPgeId
			UNION
				SELECT	lnk.cId AS LnkId,
						tre.cId AS ObjId,
						--tre.cName AS ObjName,
						dbo.[sfBaseTruncateString](tre.cName,50) AS ObjName,
						dbo.sfContGetTreDownString (tre.cId, NULL) 
							AS ObjDescription,
						'Content' AS Component,
						'Tree' AS ObjectType,
						lnk.cLocId AS LocId,
						lnk.cLngId AS LngId
				FROM	tblContTree lnk
					INNER JOIN	tblContTree tre
						ON lnk.cId = dbo.sfContGetLinkPageId (tre.cId)
				WHERE tre.cTreTpeId = 12
	RETURN
END
