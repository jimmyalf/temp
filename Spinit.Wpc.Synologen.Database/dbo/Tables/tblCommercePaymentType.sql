CREATE TABLE [dbo].[tblCommercePaymentType] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_tblCommercePaymentType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

