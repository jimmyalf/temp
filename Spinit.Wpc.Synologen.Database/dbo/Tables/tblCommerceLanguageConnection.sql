CREATE TABLE [dbo].[tblCommerceLanguageConnection] (
    [cLngId]    INT NOT NULL,
    [cPrdCatId] INT NOT NULL,
    CONSTRAINT [PK_tblCommerceLanguageConnection] PRIMARY KEY CLUSTERED ([cLngId] ASC, [cPrdCatId] ASC),
    CONSTRAINT [FK_tblCommerceLanguageConnection_tblBaseLanguages] FOREIGN KEY ([cLngId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblCommerceLanguageConnection_tblCommerceProductCategory] FOREIGN KEY ([cPrdCatId]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);

