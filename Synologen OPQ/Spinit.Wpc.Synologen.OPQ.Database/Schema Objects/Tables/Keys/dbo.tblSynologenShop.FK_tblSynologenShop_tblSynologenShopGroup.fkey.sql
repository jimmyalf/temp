ALTER TABLE [dbo].[tblSynologenShop]
    ADD CONSTRAINT [FK_tblSynologenShop_tblSynologenShopGroup] FOREIGN KEY ([cShopGroupId]) REFERENCES [dbo].[tblSynologenShopGroup] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

