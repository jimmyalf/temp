CREATE TABLE [dbo].[tblSynologenShopEquipment] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NOT NULL,
    [cDescription] NVARCHAR (500) NULL,
    CONSTRAINT [PK_tblSynologenShopEquipment] PRIMARY KEY CLUSTERED ([cId] ASC)
);

