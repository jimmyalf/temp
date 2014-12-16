CREATE TABLE [dbo].[tblCommerceProductPageConnection] (
    [cPrdId] INT NOT NULL,
    [cPgeId] INT NOT NULL,
    CONSTRAINT [PK_tblCommerceProductPageConnection] PRIMARY KEY CLUSTERED ([cPrdId] ASC, [cPgeId] ASC),
    CONSTRAINT [FK_tblCommerceProductPageConnection_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId]),
    CONSTRAINT [FK_tblCommerceProductPageConnection_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId])
);

