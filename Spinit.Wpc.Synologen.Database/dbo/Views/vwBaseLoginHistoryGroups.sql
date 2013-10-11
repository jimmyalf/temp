CREATE VIEW vwBaseLoginHistoryGroups
AS
SELECT  DISTINCT dbo.tblBaseGroups.cId,dbo.tblBaseGroups.cName,MAX(tblBaseLoginHistory.cLoginTime)  AS cLoginTime
FROM         dbo.tblBaseLoginHistory INNER JOIN
                      dbo.tblBaseUsers ON dbo.tblBaseLoginHistory.cUsrId = dbo.tblBaseUsers.cId
		 INNER JOIN dbo.tblBaseUsersGroups ON dbo.tblBaseUsers.cId = dbo.tblBaseUsersGroups.cUserId
		INNER JOIN dbo.tblBaseGroups ON dbo.tblBaseUsersGroups.cGroupId=dbo.tblBaseGroups.cId
GROUP BY dbo.tblBaseGroups.cId,dbo.tblBaseGroups.cName
