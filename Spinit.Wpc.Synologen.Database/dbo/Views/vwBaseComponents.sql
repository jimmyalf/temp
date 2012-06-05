CREATE VIEW vwBaseComponents
AS
	SELECT	tblBaseComponents.cId,
			tblBaseComponents.cName,
			tblBaseComponents.cCompInstanceTable,
			tblBaseComponents.cDescription,
			tblBaseComponents.cFromComponentList,
			tblBaseComponents.cFromWysiwyg,
			tblBaseComponents.cFramePage,
			tblBaseComponents.cEditPage,
			tblBaseComponents.cExternal	
	FROM tblBaseComponents
