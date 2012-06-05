create VIEW vwBaseLoginHistoryUsers
AS
SELECT     tblBaseUsers.cId, 
		tblBaseUsers.cUserName, 
		tblBaseUsers.cFirstName, 
		tblBaseUsers.cLastName, 
		tblBaseUsersGroups.cGroupId,	
		MAX(tblBaseLoginHistory.cLoginTime)  AS cLoginTime
FROM         dbo.tblBaseLoginHistory 
	INNER JOIN
                   dbo.tblBaseUsers ON dbo.tblBaseLoginHistory.cUsrId = dbo.tblBaseUsers.cId 
	INNER JOIN
                   dbo.tblBaseUsersGroups ON dbo.tblBaseUsers.cId = dbo.tblBaseUsersGroups.cUserId 
	
GROUP BY tblBaseUsers.cId,
		tblBaseUsers.cUserName,
		tblBaseUsers.cFirstName, 
		tblBaseUsers.cLastName, 
		tblBaseUsersGroups.cGroupId
