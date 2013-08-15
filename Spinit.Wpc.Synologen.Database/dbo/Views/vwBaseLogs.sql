create VIEW vwBaseLogs
AS
	SELECT	TOP 100 PERCENT
			tblBaseLog.cId,
			tblBaseLog.cLgTpeId,
			tblBaseLogTypes.cName AS Logtype,
			tblBaseLog.cLocId,
			tblBaseLocations.cName AS Location,
			tblBaseLog.cCmpId,
			tblBaseComponents.cName AS Component,
			tblBaseLog.cBackground,
			'Background' = 
				CASE 
					WHEN tblBaseLog.cBackground = 0
						THEN 'No'
					WHEN tblBaseLog.cBackground = 1
						THEN 'Yes'
					ELSE NULL
				END,
			tblBaseLog.cAdmin,
			'Admin' = 
				CASE
					WHEN tblBaseLog.cAdmin = 0
						THEN 'Site'
					WHEN tblBaseLog.cAdmin = 1
						THEN 'Admin'
					ELSE NULL
				END,
			tblBaseLog.cHash,
			tblBaseLog.cCount,
			tblBaseLog.cException,
			tblBaseLog.cMessage,
			tblBaseLog.cIPAddress,
			tblBaseLog.cUserAgent,
			tblBaseLog.cHttpReferrer,
			tblBaseLog.cCreatedBy,
			tblBaseLog.cCreatedDate,
			tblBaseLog.cChangedBy,
			tblBaseLog.cChangedDate,
			tblBaseLog.cClearedBy,
			tblBaseLog.cClearedDate
	FROM	tblBaseLog
		INNER JOIN tblBaseLogTypes
			ON tblBaseLog.cLgTpeId = tblBaseLogTypes.cId
		LEFT OUTER JOIN tblBaseLocations
			ON tblBaseLog.cLocId = tblBaseLocations.cId
		LEFT OUTER JOIN tblBaseComponents
			ON tblBaseLog.cCmpId = tblBaseComponents.cId
	ORDER BY	tblBaseLog.cCreatedDate ASC, 
				tblBaseLog.cChangedDate ASC, 
				tblBaseLog.cClearedDate ASC
