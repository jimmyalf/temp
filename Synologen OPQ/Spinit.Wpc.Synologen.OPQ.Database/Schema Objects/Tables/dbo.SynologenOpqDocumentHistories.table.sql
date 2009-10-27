CREATE TABLE [dbo].[SynologenOpqDocumentHistories] (
    [Id]              INT            NOT NULL,
    [HistoryDate]     DATETIME       NOT NULL,
    [HistoryId]       INT            NOT NULL,
    [HistoryName]     NVARCHAR (100) NOT NULL,
    [NdeId]           INT            NOT NULL,
    [ShpId]           INT            NULL,
    [CncId]           INT            NULL,
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
    [LockedDate]      DATETIME       NULL
);





