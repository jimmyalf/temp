ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [FK_SynologenOpqDocuments_tblSynologenShopGroup] FOREIGN KEY ([ShopGroupId]) REFERENCES [dbo].[tblSynologenShopGroup] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

