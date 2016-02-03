CREATE TABLE [dbo].[SynologenOrderCustomer] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ShopId]           INT            NOT NULL,
    [FirstName]        NVARCHAR (50)  NOT NULL,
    [LastName]         NVARCHAR (50)  NOT NULL,
    [PersonalIdNumber] NVARCHAR (12)  NOT NULL,
    [Email]            NVARCHAR (50)  NULL,
    [MobilePhone]      NVARCHAR (50)  NULL,
    [Phone]            NVARCHAR (50)  NULL,
    [AddressLineOne]   NVARCHAR (50)  NOT NULL,
    [AddressLineTwo]   NVARCHAR (50)  NULL,
    [City]             NVARCHAR (50)  NOT NULL,
    [PostalCode]       NVARCHAR (50)  NOT NULL,
    [Notes]            NVARCHAR (512) NULL,
    CONSTRAINT [PK_SynologenOrderCustomer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderCustomer_tblSynologenShop] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

