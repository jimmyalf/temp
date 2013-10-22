create VIEW SynologenOpqDocumentView
--CREATE VIEW [dbo].[SynologenOpqDocumentView]
AS 
		SELECT  Id,
				NULL AS HistoryDate,
				NdeId,
				ShpId,
				CncId,
				DocTpeId,
				DocumentContent,
				IsActive,
				ApprovedById,
				ApprovedByName,
				ApprovedDate,
				LockedById,
				LockedByName,
				LockedDate
		FROM	dbo.SynologenOpqDocuments
	UNION ALL
		SELECT  Id,
				HistoryDate,
				NdeId,
				ShpId,
				CncId,
				DocTpeId,
				DocumentContent,
				IsActive,
				ApprovedById,
				ApprovedByName,
				ApprovedDate,
				LockedById,
				LockedByName,
				LockedDate
		FROM	dbo.SynologenOpqDocumentHistories
