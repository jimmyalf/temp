CREATE TABLE [dbo].[tblCommerceOrderItem] (
    [cId]           INT           IDENTITY (1, 1) NOT NULL,
    [cOrdStsId]     INT           NOT NULL,
    [cOrdId]        INT           NOT NULL,
    [cPrdId]        INT           NOT NULL,
    [cNoOfProducts] INT           NOT NULL,
    [cPrice]        MONEY         NOT NULL,
    [cSum]          MONEY         NOT NULL,
    [cCrnCde]       NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_tblCommerceOrderItem] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCommerceOrderItem_tblCommerceCurrency] FOREIGN KEY ([cCrnCde]) REFERENCES [dbo].[tblCommerceCurrency] ([cCurrencyCode]),
    CONSTRAINT [FK_tblCommerceOrderItem_tblCommerceOrder] FOREIGN KEY ([cOrdId]) REFERENCES [dbo].[tblCommerceOrder] ([cId]),
    CONSTRAINT [FK_tblCommerceOrderItem_tblCommerceOrderStatus] FOREIGN KEY ([cOrdStsId]) REFERENCES [dbo].[tblCommerceOrderStatus] ([cId])
);

