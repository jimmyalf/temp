ALTER TABLE [dbo].[tblSynologenShopMemberConnection]
    ADD CONSTRAINT [FK_tblSynologenShopMemberConnection_tblSynologenShop] FOREIGN KEY ([cSynologenShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

