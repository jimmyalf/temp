CREATE TABLE [dbo].[tblDocumentNodeLanguageConnection] (
    [cDocumentNodeId] INT NOT NULL,
    [cLanguageId]     INT NOT NULL,
    CONSTRAINT [PK_tblDocumentLanguageConnection] PRIMARY KEY CLUSTERED ([cDocumentNodeId] ASC, [cLanguageId] ASC),
    CONSTRAINT [FK_tblDocumentNodeLanguageConnection_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblDocumentNodeLanguageConnection_tblDocumentNode] FOREIGN KEY ([cDocumentNodeId]) REFERENCES [dbo].[tblDocumentNode] ([cId])
);

