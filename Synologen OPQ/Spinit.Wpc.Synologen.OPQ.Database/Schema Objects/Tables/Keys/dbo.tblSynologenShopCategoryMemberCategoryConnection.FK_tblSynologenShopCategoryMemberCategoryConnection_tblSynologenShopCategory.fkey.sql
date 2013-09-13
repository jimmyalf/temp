ALTER TABLE [dbo].[tblSynologenShopCategoryMemberCategoryConnection]
    ADD CONSTRAINT [FK_tblSynologenShopCategoryMemberCategoryConnection_tblSynologenShopCategory] FOREIGN KEY ([cShopCategoryId]) REFERENCES [dbo].[tblSynologenShopCategory] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

