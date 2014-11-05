CREATE TABLE [dbo].[SynologenOpqDocuments] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [NdeId]           INT            NOT NULL,
    [ShpId]           INT            NULL,
    [CncId]           INT            NULL,
    [ShopGroupId]     INT            NULL,
    [DocTpeId]        INT            NOT NULL,
    [DocumentContent] NTEXT          NOT NULL,
    [IsActive]        BIT            NOT NULL,
    [CreatedById]     INT            NOT NULL,
    [CreatedByName]   NVARCHAR (100) NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [ChangedById]     INT            NULL,
    [ChangedByName]   NVARCHAR (100) NULL,
    [ChangedDate]     DATETIME       NULL,
    [ApprovedById]    INT            NULL,
    [ApprovedByName]  NVARCHAR (100) NULL,
    [ApprovedDate]    DATETIME       NULL,
    [LockedById]      INT            NULL,
    [LockedByName]    NVARCHAR (100) NULL,
    [LockedDate]      DATETIME       NULL,
    CONSTRAINT [SynologenOpqDocuments_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOpqDocuments_tblSynologenShopGroup] FOREIGN KEY ([ShopGroupId]) REFERENCES [dbo].[tblSynologenShopGroup] ([Id]),
    CONSTRAINT [SynologenOpqDocumentTypes_SynologenOpqDocuments_FK1] FOREIGN KEY ([DocTpeId]) REFERENCES [dbo].[SynologenOpqDocumentTypes] ([Id]),
    CONSTRAINT [SynologenOpqNodes_SynologenOpqDocuments_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]),
    CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK3] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblSynologenConcern_SynologenOpqDocuments_FK1] FOREIGN KEY ([CncId]) REFERENCES [dbo].[tblSynologenConcern] ([cId]),
    CONSTRAINT [tblSynologenShop_SynologenOpqDocuments_FK1] FOREIGN KEY ([ShpId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);




GO
-- =============================================
-- Instead-of update document
-- =============================================

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
