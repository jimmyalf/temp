-- =============================================
-- Instead-of update document
-- =============================================

--ALTER TRIGGER [InsteadOfUpdate] 
CREATE TRIGGER [InsteadOfUpdate] 
ON [dbo].[SynologenOpqDocuments]
INSTEAD OF UPDATE
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @documentId INT,
			@historyId INT,
			@historyName NVARCHAR (100)
	
	SELECT	@documentId = Id,
			@historyId = ChangedById,
			@historyName = ChangedByName
	FROM	INSERTED
	
	UPDATE	dbo.SynologenOpqDocuments
	SET		NdeId = Document.NdeId,
			DocTpeId = Document.DocTpeId,
			Document = Document.Document,
			IsActive = Document.IsActive,
			CreatedById = Document.CreatedById,
			CreatedByName = Document.CreatedByName,
			CreatedDate = Document.CreatedDate,
			ChangedById = Document.ChangedById,
			ChangedByName = Document.ChangedByName,
			ChangedDate = Document.ChangedDate,
			ApprovedById = Document.ApprovedById,
			ApprovedByName = Document.ApprovedByName,
			ApprovedDate = Document.ApprovedDate,
			LockedById = Document.LockedById,
			LockedByName = Document.LockedByName,
			LockedDate = Document.LockedDate
	FROM (SELECT	NdeId, DocTpeId, Document, IsActive, CreatedById, CreatedByName, CreatedDate,
				ChangedById, ChangedByName, ChangedDate, ApprovedById, ApprovedByName, ApprovedDate,
				LockedById, LockedByName, LockedDate
		 FROM INSERTED) AS Document
	WHERE	Id = @documentId

	INSERT INTO dbo.SynologenOpqDocumentHistories (
		Id, HistoryDate, HistoryId, HistoryName, NdeId, DocTpeId, Document, IsActive, CreatedById, CreatedByName, CreatedDate,
		ChangedById, ChangedByName, ChangedDate, ApprovedById, ApprovedByName, ApprovedDate,
		LockedById, LockedByName, LockedDate)
	SELECT
		Id,
		GETDATE (),
		@historyId,
		@historyName,
		NdeId,
		DocTpeId,
		Document,
		IsActive,
		CreatedById,
		CreatedByName,
		CreatedDate,
		ChangedById,
		ChangedByName,
		ChangedDate,
		ApprovedById,
		ApprovedByName,
		ApprovedDate,
		LockedById,
		LockedByName,
		LockedDate
	FROM DELETED

END