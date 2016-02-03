CREATE TABLE [dbo].[tblSynologenShopCategoryMemberCategoryConnection] (
    [cShopCategoryId]   INT NOT NULL,
    [cMemberCategoryId] INT NOT NULL,
    CONSTRAINT [PK_tblShopCategoryMemberCategoryConnection] PRIMARY KEY CLUSTERED ([cShopCategoryId] ASC, [cMemberCategoryId] ASC),
    CONSTRAINT [FK_tblSynologenShopCategoryMemberCategoryConnection_tblMemberCategories] FOREIGN KEY ([cMemberCategoryId]) REFERENCES [dbo].[tblMemberCategories] ([cId]),
    CONSTRAINT [FK_tblSynologenShopCategoryMemberCategoryConnection_tblSynologenShopCategory] FOREIGN KEY ([cShopCategoryId]) REFERENCES [dbo].[tblSynologenShopCategory] ([cId])
);

