CREATE TABLE [dbo].[tblCommerceOrder] (
    [cId]           INT            IDENTITY (1, 1) NOT NULL,
    [cOrdStsId]     INT            NOT NULL,
    [cCustomerId]   INT            NOT NULL,
    [cCustomerType] INT            NOT NULL,
    [cSum]          MONEY          NOT NULL,
    [cOrderDate]    DATETIME       NOT NULL,
    [cPayTpeId]     INT            NOT NULL,
    [cHandledBy]    NVARCHAR (100) NULL,
    [cHandledDate]  DATETIME       NULL,
    [cDelTpeId]     INT            NULL,
    [cDeliveryDate] DATETIME       NOT NULL,
    CONSTRAINT [PK_tblCommerceOrder] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCommerceOrder_tblCommerceDeliveryType] FOREIGN KEY ([cDelTpeId]) REFERENCES [dbo].[tblCommerceDeliveryType] ([cId]),
    CONSTRAINT [FK_tblCommerceOrder_tblCommerceOrderStatus] FOREIGN KEY ([cOrdStsId]) REFERENCES [dbo].[tblCommerceOrderStatus] ([cId]),
    CONSTRAINT [FK_tblCommerceOrder_tblCommercePaymentType] FOREIGN KEY ([cPayTpeId]) REFERENCES [dbo].[tblCommercePaymentType] ([cId])
);

