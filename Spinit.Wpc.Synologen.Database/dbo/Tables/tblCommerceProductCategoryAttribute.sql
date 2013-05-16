CREATE TABLE [dbo].[tblCommerceProductCategoryAttribute] (
    [cPrdCatId] INT             NOT NULL,
    [cAttId]    INT             NOT NULL,
    [cOrder]    INT             NOT NULL,
    [cValue]    NVARCHAR (1024) NULL,
    CONSTRAINT [PK_tblCommerceProductCategoryAttribute] PRIMARY KEY CLUSTERED ([cPrdCatId] ASC, [cAttId] ASC),
    CONSTRAINT [FK_tblCommerceProductCategoryAttribute_tblCommerceAttribute] FOREIGN KEY ([cAttId]) REFERENCES [dbo].[tblCommerceAttribute] ([cId]),
    CONSTRAINT [FK_tblCommerceProductCategoryAttribute_tblCommerceProductCategory] FOREIGN KEY ([cPrdCatId]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);

