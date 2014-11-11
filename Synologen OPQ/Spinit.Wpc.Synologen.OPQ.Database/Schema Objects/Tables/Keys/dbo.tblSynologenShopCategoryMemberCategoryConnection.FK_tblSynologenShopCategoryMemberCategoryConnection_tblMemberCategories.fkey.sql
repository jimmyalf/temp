ALTER TABLE [dbo].[tblSynologenShopCategoryMemberCategoryConnection]
    ADD CONSTRAINT [FK_tblSynologenShopCategoryMemberCategoryConnection_tblMemberCategories] FOREIGN KEY ([cMemberCategoryId]) REFERENCES [dbo].[tblMemberCategories] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



