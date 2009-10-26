ALTER TABLE [dbo].[tblSynologenShop]
    ADD CONSTRAINT [FK_tblSynologenShop_tblSynologenShopCategory] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblSynologenShopCategory] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



