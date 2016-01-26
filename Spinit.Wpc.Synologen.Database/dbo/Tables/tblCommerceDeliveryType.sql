CREATE TABLE [dbo].[tblCommerceDeliveryType] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_tblCommerceDeliveryType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

