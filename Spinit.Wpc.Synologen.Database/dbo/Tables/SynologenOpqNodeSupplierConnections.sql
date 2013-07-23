CREATE TABLE [dbo].[SynologenOpqNodeSupplierConnections] (
    [NdeId] INT NOT NULL,
    [SupId] INT NOT NULL,
    CONSTRAINT [SynologenOpqNodeSupplierConnections_PK] PRIMARY KEY CLUSTERED ([NdeId] ASC, [SupId] ASC),
    CONSTRAINT [SynologenOpqNodes_SynologenOpqNodeSupplierConnections_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]),
    CONSTRAINT [tblBaseUsers_SynologenOpqNodeSupplierConnections_FK1] FOREIGN KEY ([SupId]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);

