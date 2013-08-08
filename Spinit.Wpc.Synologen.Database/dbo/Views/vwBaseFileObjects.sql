create VIEW vwBaseFileObjects
AS
		SELECT	tblBaseFile.cId AS FleId,
				tblContPage.cId AS ObjId,
				tblContPage.cName AS ObjName,
				NULL AS ObjDescription
		FROM	tblBaseFile
			INNER JOIN	tblContPageFile
				ON tblBaseFile.cId = tblContPageFile.cFleId
			INNER JOIN	tblContPage
				ON tblContPageFile.cPgeId = tblContPage.cId
		WHERE	tblContPage.cId NOT IN (SELECT cPgeId FROM tblContTree)
	UNION
		SELECT	tblBaseFile.cId AS FleId,
				tblContTree.cId AS ObjId,
				tblContTree.cName AS ObjName,
				dbo.sfContGetTreDownString (tblContTree.cId, NULL) 
					AS ObjDescription
		FROM	tblBaseFile
			INNER JOIN	tblContPageFile
				ON tblBaseFile.cId = tblContPageFile.cFleId
			INNER JOIN	tblContTree
				ON tblContPageFile.cPgeId = tblContTree.cPgeId
