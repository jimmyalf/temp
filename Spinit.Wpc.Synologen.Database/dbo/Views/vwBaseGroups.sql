create VIEW vwBaseGroups
AS
	SELECT	tblBaseGroups.cId,
			tblBaseGroups.cName,
			tblBaseGroups.cDescription,
			tblBaseGroups.cGrpTpeId,
			tblBaseGroups.cCreatedBy,
			tblBaseGroups.cCreatedDate,
			tblBaseGroups.cChangedBy,
			tblBaseGroups.cChangedDate,
			tblBaseGroupType.cId  AS GrpTpeId,
			tblBaseGroupType.cName AS GrpTpeName,
			tblBaseGroupType.cDescription AS GrpTpeDescription
	FROM	tblBaseGroups
		INNER JOIN tblBaseGroupType
			ON tblBaseGroups.cGrpTpeId = tblBaseGroupType.cId
