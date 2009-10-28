CREATE VIEW [dbo].[DocumentView]
AS 
		SELECT  Id,
				NULL AS HistoryDate,
				IsActive,
				ApprovedById,
				ApprovedByName,
				ApprovedDate,
				LockedById,
				LockedByName,
				LockedDate
		FROM	dbo.SynologenOpqDocuments
	UNION
		SELECT  Id,
				HistoryDate,
				IsActive,
				ApprovedById,
				ApprovedByName,
				ApprovedDate,
				LockedById,
				LockedByName,
				LockedDate
		FROM	dbo.SynologenOpqDocumentHistories
