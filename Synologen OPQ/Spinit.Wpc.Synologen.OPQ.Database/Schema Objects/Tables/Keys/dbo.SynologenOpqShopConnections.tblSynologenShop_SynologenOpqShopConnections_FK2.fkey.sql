ALTER TABLE [dbo].[SynologenOpqShopConnections]
    ADD CONSTRAINT [tblSynologenShop_SynologenOpqShopConnections_FK2] FOREIGN KEY ([ShpId2]) REFERENCES [dbo].[tblSynologenShop] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

