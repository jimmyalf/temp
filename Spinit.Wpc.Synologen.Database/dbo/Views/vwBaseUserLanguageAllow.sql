CREATE VIEW vwBaseUserLanguageAllow
AS
		SELECT	tblBaseUsers.cId AS UserId,
				tblBaseUsers.cUserName,
				tblBaseUsers.cFirstName 
					+ ' ' 
					+ tblBaseusers.cLastName AS UserName,
				tblBaseUsers.cEmail,
				tblBaseUsers.cActive,
				tblBaseLocations.cId AS LocationId,
				tblBaseLocations.cName AS LocationName,
				tblBaseLanguages.cId AS LanguageId,
				tblBaseLanguages.cName AS LanguageName
		FROM	tblBaseLocations, tblBaseLanguages, tblBaseUsers
			INNER JOIN tblBaseUsersGroups
				ON tblBaseUsers.cId = tblBaseUsersGroups.cUserId
			INNER JOIN tblBaseGroups
				ON tblBaseUsersGroups.cGroupId = tblBaseGroups.cId
		WHERE	(tblBaseGroups.cGrpTpeId = 1
				OR	tblBaseGroups.cGrpTpeId = 2)
			AND tblBaseLanguages.cId IN (SELECT 	cLanguageId 
							FROM	tblBaseLocationsLanguages
							WHERE	cLocationId = tblBaseLocations.cId)
	UNION
		SELECT	tblBaseUsers.cId AS UserId,
				tblBaseUsers.cUserName,
				tblBaseUsers.cFirstName 
					+ ' ' 
					+ tblBaseusers.cLastName AS UserName,
				tblBaseUsers.cEmail,
				tblBaseUsers.cActive,
				tblBaseLocations.cId AS LocationId,
				tblBaseLocations.cName AS LocationName,
				tblBaseLanguages.cId AS LanguageId,
				tblBaseLanguages.cName AS LanguageName
		FROM	tblBaseUsers
			INNER JOIN tblBaseUsersGroups
				ON tblBaseUsers.cId = tblBaseUsersGroups.cUserId
			INNER JOIN tblBaseGroups
				ON tblBaseUsersGroups.cGroupId = tblBaseGroups.cId
			INNER JOIN tblBaseGroupsLocations
				ON tblBaseGroupsLocations.cGroupId = tblBaseGroups.cId
			INNER JOIN tblBaseLocations
				ON tblBaseLocations.cId = tblBaseGroupsLocations.cLocationId
			INNER JOIN tblBaseLocationsLanguages
				ON tblBaseLocationsLanguages.cLocationId = tblBaseLocations.cId
			INNER JOIN tblBaseLanguages
				ON tblBaseLanguages.cId = tblBaseLocationsLanguages.cLanguageId
		WHERE	tblBaseGroups.cGrpTpeId = 3	
	UNION
		SELECT	tblBaseUsers.cId AS UserId,
				tblBaseUsers.cUserName,
				tblBaseUsers.cFirstName 
					+ ' ' 
					+ tblBaseusers.cLastName AS UserName,
				tblBaseUsers.cEmail,
				tblBaseUsers.cActive,
				tblBaseLocations.cId AS LocationId,
				tblBaseLocations.cName AS LocationName,
				tblBaseLanguages.cId AS LanguageId,
				tblBaseLanguages.cName AS LanguageName
		FROM	tblBaseUsers
			INNER JOIN tblBaseUsersGroups
				ON tblBaseUsers.cId = tblBaseUsersGroups.cUserId
			INNER JOIN tblBaseGroups
				ON tblBaseUsersGroups.cGroupId = tblBaseGroups.cId
			INNER JOIN tblBaseGroupsLanguages
				ON tblBaseGroupsLanguages.cGroupId = tblBaseGroups.cId
			INNER JOIN tblBaseLocations
				ON tblBaseLocations.cId = tblBaseGroupsLanguages.cLocationId
			INNER JOIN tblBaseLanguages
				ON tblBaseLanguages.cId = tblBaseGroupsLanguages.cLanguageId
		WHERE	tblBaseGroups.cGrpTpeId = 4
			OR	tblBaseGroups.cGrpTpeId = 5
