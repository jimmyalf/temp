CREATE TABLE [dbo].[tblDocumentDocumentFileConnection] (
    [cDocumentId] INT NOT NULL,
    [cFileId]     INT NOT NULL,
    CONSTRAINT [PK_tblDocumentDocumentFileConnection] PRIMARY KEY CLUSTERED ([cDocumentId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblDocumentDocumentFileConnection_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblDocumentDocumentFileConnection_tblDocuments] FOREIGN KEY ([cDocumentId]) REFERENCES [dbo].[tblDocuments] ([cId])
);

