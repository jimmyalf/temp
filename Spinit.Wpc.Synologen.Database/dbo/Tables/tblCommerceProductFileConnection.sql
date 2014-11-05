CREATE TABLE [dbo].[tblCommerceProductFileConnection] (
    [cPrdId] INT NOT NULL,
    [cFleId] INT NOT NULL,
    CONSTRAINT [PK_tblCommerceProductFileConnection] PRIMARY KEY CLUSTERED ([cPrdId] ASC, [cFleId] ASC),
    CONSTRAINT [FK_tblCommerceProductFileConnection_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCommerceProductFileConnection_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId])
);

