CREATE TABLE [dbo].[tblCommerceOrderStatus] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (512) NULL,
    CONSTRAINT [PK_tblCommerceOrderStatus] PRIMARY KEY CLUSTERED ([cId] ASC)
);

