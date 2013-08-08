CREATE VIEW vwBaseUserObjectsDeny
AS
	SELECT	tblBaseUsers.cId AS UserId,
			tblBaseUsers.cUserName,
			tblBaseUsers.cFirstName + ' ' + tblBaseusers.cLastName AS UserName,
			tblBaseUsers.cEmail,
			tblBaseUsers.cActive,
			tblBaseGroups.cId AS GroupId,
			tblBaseGroups.cName AS GroupName,
			tblBaseGroupType.cId AS GroupTypeId,
			tblBaseGroupType.cName AS GroupTypeName,
			tblBaseObjects.cId AS ObjectId,
			tblBaseObjects.cName AS ObjectName,
			tblBaseObjectType.cId AS ObjectTypeId,
			tblBaseObjectType.cId AS ObjectTypeName,
			tblBaseComponents.cId AS ComponentId,
			tblBaseComponents.cName AS ComponentName
	FROM	tblBaseUsers
		INNER JOIN tblBaseUsersGroups
			ON tblBaseUsers.cId = tblBaseUsersGroups.cUserId
		INNER JOIN tblBaseGroups
			ON tblBaseUsersGroups.cGroupId = tblBaseGroups.cId
		INNER JOIN tblBaseGroupType
			ON tblBaseGroups.cGrpTpeId = tblBaseGroupType.cId
		INNER JOIN tblBaseGroupsObjects
			ON tblBaseGroups.cId = tblBaseGroupsObjects.cGroupId
		INNER JOIN tblBaseObjects
			ON tblBaseGroupsObjects.cObjectId = tblBaseObjects.cId
		INNER JOIN tblBaseObjectType
			ON tblBaseGroupsObjects.cObjTpeId = tblBaseObjectType.cId
		INNER JOIN tblBaseComponents
			ON tblBaseObjects.cCmpId = tblBaseComponents.cId
		INNER JOIN tblBaseGroupsLocations
			ON tblBaseGroups.cId = tblBaseGroupsLocations.cGroupId
		INNER JOIN tblBaseLocations
			ON tblBaseGroupsLocations.cLocationId = tblBaseLocations.cId
		INNER JOIN tblBaseGroupsLanguages
			ON tblBaseGroups.cId = tblBaseGroupsLanguages.cGroupId
		INNER JOIN tblBaseLanguages
			ON tblBaseGroupsLanguages.cLanguageId = tblBaseLanguages.cId
	WHERE	tblBaseGroupType.cId = 5
		AND	tblBaseGroupsObjects.cObjTpeId = 3
