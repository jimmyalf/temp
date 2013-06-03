CREATE TABLE [dbo].[tblDocumentNodeFileConnection] (
    [cDocumentNodeId] INT NOT NULL,
    [cFileId]         INT NOT NULL,
    CONSTRAINT [PK_tblDocumentNodeFileConnection] PRIMARY KEY CLUSTERED ([cDocumentNodeId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblDocumentNodeFileConnection_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblDocumentNodeFileConnection_tblDocumentNode] FOREIGN KEY ([cDocumentNodeId]) REFERENCES [dbo].[tblDocumentNode] ([cId])
);

