ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblSynologenShop_SynologenOpqDocuments_FK1] FOREIGN KEY ([ShpId]) REFERENCES [dbo].[tblSynologenShop] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

