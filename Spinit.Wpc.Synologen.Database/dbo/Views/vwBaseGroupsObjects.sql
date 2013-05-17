CREATE VIEW vwBaseGroupsObjects
AS
	SELECT	cGroupId,
			cObjectId,
			cObjTpeId,
			tblBaseGroups.cName AS GroupName,
			tblBaseGroups.cDescription AS GroupDescription,
			tblBaseObjects.cName AS ObjectName,
			tblBaseObjects.cDescription AS ObjectDescription,
			tblBaseObjectType.cName AS ObjectTypeName,
			tblBaseObjectType.cDescription AS ObjectTypeDescription
	FROM	tblBaseGroupsObjects
		INNER JOIN tblBaseObjectType
			ON tblBaseGroupsObjects.cObjTpeId = tblBaseObjectType.cId
		INNER JOIN tblBaseGroups
			ON tblBaseGroupsObjects.cGroupId = tblBaseGroups.cId
		INNER JOIN tblBaseObjects
			ON tblBaseGroupsObjects.cObjectId = tblBaseObjects.cId
