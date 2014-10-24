create VIEW vwBaseUserNoPwd
AS
	SELECT	cId,
			cUserName,
			cFirstName,
			cLastName,
			cEmail,
			cDefaultLocation,
			cActive,
			cCreatedBy,
			cCreatedDate,
			cChangedBy,
			cChangedDate
	FROM	tblBaseUsers
