-- =============================================
-- Instead-of update document
-- =============================================

--ALTER TRIGGER [SynologenOpqDocuments_InsteadOfUpdate] 
CREATE TRIGGER SynologenOpqDocuments_InsteadOfUpdate 
--CREATE TRIGGER [SynologenOpqDocuments_InsteadOfUpdate] 
ON dbo.SynologenOpqDocuments
INSTEAD OF UPDATE
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @documentId INT,
			@documentTypeId INT,
			@historyId INT,
			@historyName NVARCHAR (100),
			@oldIsActive BIT,
			@newIsActive BIT,
			@oldApprovedById INT,
			@newApprovedById INT,
			@oldLockedById INT,
			@newLockedById INT
	
	SELECT	@documentId = Id,
			@documentTypeId = DocTpeId,
			@newIsActive = IsActive,
			@historyId = ChangedById,
			@historyName = ChangedByName,
			@newApprovedById = ApprovedById,
			@newLockedById = LockedById
	FROM	INSERTED
	
	SELECT	@oldIsActive = IsActive,
			@oldApprovedById = ApprovedById,
			@oldLockedById = LockedById
	FROM	DELETED
	
	UPDATE	dbo.SynologenOpqDocuments
	SET		NdeId = Document.NdeId,
			ShpId = Document.ShpId,
			CncId = Document.CncId,
			ShopGroupId = Document.ShopGroupId,
			DocTpeId = Document.DocTpeId,
			DocumentContent = Document.DocumentContent,
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
	FROM (SELECT NdeId, ShpId, CncId, ShopGroupId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
				 ChangedById, ChangedByName, ChangedDate, ApprovedById, ApprovedByName, ApprovedDate,
				 LockedById, LockedByName, LockedDate
		 FROM INSERTED) AS Document
	WHERE	Id = @documentId
		
	IF (@documentTypeId = 1)																		-- Routine
		AND ((@oldIsActive = 1) AND (@oldApprovedById IS NOT NULL) AND (@oldLockedById IS NULL))	-- Old doc approved, not locked and active
		AND ((@newIsActive = 0) OR (@newApprovedById IS NULL) OR (@newLockedById IS NOT NULL))		-- New doc not approved, locked or not active
		BEGIN
			INSERT INTO dbo.SynologenOpqDocumentHistories (
				Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, ShopGroupId, DocTpeId, DocumentContent, IsActive, 
				CreatedById, CreatedByName, CreatedDate, ChangedById, ChangedByName, ChangedDate, 
				ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate)
			SELECT
				Id,
				GETDATE (),
				@historyId,
				@historyName,
				NdeId,
				ShpId,
				CncId,
				ShopGroupId,
				DocTpeId,
				DocumentContent,
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
END