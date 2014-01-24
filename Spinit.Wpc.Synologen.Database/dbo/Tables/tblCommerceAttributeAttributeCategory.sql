CREATE TABLE [dbo].[tblCommerceAttributeAttributeCategory] (
    [cAttCatId] INT NOT NULL,
    [cAttId]    INT NOT NULL,
    CONSTRAINT [PK_tblCommerceAttributeAttributeCategory] PRIMARY KEY CLUSTERED ([cAttCatId] ASC, [cAttId] ASC),
    CONSTRAINT [FK_tblCommerceAttributeAttributeCategory_tblCommerceAttribute] FOREIGN KEY ([cAttId]) REFERENCES [dbo].[tblCommerceAttribute] ([cId]),
    CONSTRAINT [FK_tblCommerceAttributeAttributeCategory_tblCommerceAttributeCategory] FOREIGN KEY ([cAttCatId]) REFERENCES [dbo].[tblCommerceAttributeCategory] ([cId])
);

