CREATE TABLE [dbo].[tblSynologenCountry] (
    [cId]                    INT           IDENTITY (1, 1) NOT NULL,
    [cName]                  NVARCHAR (50) NOT NULL,
    [cSvefakturaCountryCode] INT           NOT NULL,
    CONSTRAINT [PK_tblSynologenCountry] PRIMARY KEY CLUSTERED ([cId] ASC)
);

