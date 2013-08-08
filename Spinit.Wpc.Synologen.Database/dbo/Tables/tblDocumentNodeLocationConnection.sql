CREATE TABLE [dbo].[tblDocumentNodeLocationConnection] (
    [cDocumentNodeId] INT NOT NULL,
    [cLocationId]     INT NOT NULL,
    CONSTRAINT [PK_tblDocumentLocationConnection] PRIMARY KEY CLUSTERED ([cDocumentNodeId] ASC, [cLocationId] ASC),
    CONSTRAINT [FK_tblDocumentNodeLocationConnection_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblDocumentNodeLocationConnection_tblDocumentNode] FOREIGN KEY ([cDocumentNodeId]) REFERENCES [dbo].[tblDocumentNode] ([cId])
);

