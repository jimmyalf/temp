CREATE TABLE [dbo].[tblCommerceCustomerData] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cFirstName]   NVARCHAR (100) NULL,
    [cLastName]    NVARCHAR (100) NULL,
    [cCompany]     NVARCHAR (256) NULL,
    [cStreet]      NVARCHAR (256) NULL,
    [cPostCode]    NVARCHAR (10)  NULL,
    [cCity]        NVARCHAR (100) NULL,
    [cCountry]     NVARCHAR (100) NULL,
    [cEmail]       NVARCHAR (512) NULL,
    [cPhone]       NVARCHAR (20)  NULL,
    [cDelStreet]   NVARCHAR (256) NULL,
    [cDelPostCode] NVARCHAR (20)  NULL,
    [cDelCity]     NVARCHAR (100) NULL,
    [cDelCountry]  NVARCHAR (100) NULL,
    [cCreatedDate] DATETIME       NULL,
    CONSTRAINT [PK_tblCommerceCustomerData] PRIMARY KEY CLUSTERED ([cId] ASC)
);

