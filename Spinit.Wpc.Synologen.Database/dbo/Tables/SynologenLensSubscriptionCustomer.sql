CREATE TABLE [dbo].[SynologenLensSubscriptionCustomer] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (100)  NOT NULL,
    [LastName]         NVARCHAR (100)  NOT NULL,
    [AddressLineOne]   NVARCHAR (100)  NOT NULL,
    [AddressLineTwo]   NVARCHAR (100)  NULL,
    [City]             NVARCHAR (100)  NOT NULL,
    [PostalCode]       NVARCHAR (50)   NOT NULL,
    [CountryId]        INT             NOT NULL,
    [Email]            NVARCHAR (100)  NULL,
    [MobilePhone]      NVARCHAR (100)  NULL,
    [Phone]            NVARCHAR (100)  NULL,
    [PersonalIdNumber] NCHAR (12)      NOT NULL,
    [ShopId]           INT             NOT NULL,
    [Notes]            NVARCHAR (4000) NULL,
    CONSTRAINT [PK_SynologenLensSubscriptionCustomer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenLensSubscriptionCustomer_tblSynologenCountry] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[tblSynologenCountry] ([cId]),
    CONSTRAINT [FK_SynologenLensSubscriptionCustomer_tblSynologenShop] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

