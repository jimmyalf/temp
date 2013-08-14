CREATE TABLE [dbo].[tblSynologenShopEquipmentConnection] (
    [cId]              INT            IDENTITY (1, 1) NOT NULL,
    [cShopEquipmentId] INT            NOT NULL,
    [cShopId]          INT            NOT NULL,
    [cNotes]           NVARCHAR (255) NULL,
    CONSTRAINT [PK_tblSynologenShopEquipmentConnection] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblSynologenShopEquipmentConnection_tblSynologenShop] FOREIGN KEY ([cShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId]),
    CONSTRAINT [FK_tblSynologenShopEquipmentConnection_tblSynologenShopEquipment] FOREIGN KEY ([cShopEquipmentId]) REFERENCES [dbo].[tblSynologenShopEquipment] ([cId])
);

