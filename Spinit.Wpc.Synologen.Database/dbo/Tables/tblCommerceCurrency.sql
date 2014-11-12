CREATE TABLE [dbo].[tblCommerceCurrency] (
    [cCurrencyCode] NVARCHAR (10)  NOT NULL,
    [cName]         NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_tblCommerceCurrency] PRIMARY KEY CLUSTERED ([cCurrencyCode] ASC)
);

