CREATE TABLE [dbo].[tblCommerceProductCategoryFile] (
    [cPrdCatId] INT NOT NULL,
    [cFleId]    INT NOT NULL,
    [cLngId]    INT NOT NULL,
    CONSTRAINT [PK_tblCommerceProductCategoryFile] PRIMARY KEY CLUSTERED ([cPrdCatId] ASC, [cFleId] ASC, [cLngId] ASC),
    CONSTRAINT [FK_tblCommerceProductCategoryFile_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCommerceProductCategoryFile_tblBaseLanguages] FOREIGN KEY ([cLngId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblCommerceProductCategoryFile_tblCommerceProductCategory] FOREIGN KEY ([cPrdCatId]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);

