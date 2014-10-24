CREATE TABLE [dbo].[SynologenOpqDocumentHistories] (
    [Id]              INT            NOT NULL,
    [HistoryDate]     DATETIME       NOT NULL,
    [HistoryId]       INT            NOT NULL,
    [HistoryName]     NVARCHAR (100) NOT NULL,
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
    CONSTRAINT [SynologenOpqDocumentHistories_PK] PRIMARY KEY CLUSTERED ([Id] ASC, [HistoryDate] ASC),
    CONSTRAINT [SynologenOpqDocuments_SynologenOpqDocumentHistories_FK1] FOREIGN KEY ([Id]) REFERENCES [dbo].[SynologenOpqDocuments] ([Id]),
    CONSTRAINT [tblBaseUsers_SynologenOpqDocumentHistories_FK1] FOREIGN KEY ([HistoryId]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);



