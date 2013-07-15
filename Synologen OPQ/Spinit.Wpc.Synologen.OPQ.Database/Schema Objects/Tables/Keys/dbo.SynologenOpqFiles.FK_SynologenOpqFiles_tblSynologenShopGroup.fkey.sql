ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [FK_SynologenOpqFiles_tblSynologenShopGroup] FOREIGN KEY ([ShopGroupId]) REFERENCES [dbo].[tblSynologenShopGroup] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

