CREATE TABLE [dbo].[tblCommerceProductPrice] (
    [cPrdId]  INT           NOT NULL,
    [cCrnCde] NVARCHAR (10) NOT NULL,
    [cPrice]  MONEY         NOT NULL,
    CONSTRAINT [PK_tblCommerceProductPrice] PRIMARY KEY CLUSTERED ([cPrdId] ASC, [cCrnCde] ASC),
    CONSTRAINT [FK_tblCommerceProductPrice_tblCommerceCurrency] FOREIGN KEY ([cCrnCde]) REFERENCES [dbo].[tblCommerceCurrency] ([cCurrencyCode]),
    CONSTRAINT [FK_tblCommerceProductPrice_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId])
);

