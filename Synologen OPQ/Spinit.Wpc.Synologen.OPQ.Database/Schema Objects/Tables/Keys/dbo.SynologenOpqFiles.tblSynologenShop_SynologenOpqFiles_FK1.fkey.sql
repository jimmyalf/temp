ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblSynologenShop_SynologenOpqFiles_FK1] FOREIGN KEY ([ShpId]) REFERENCES [dbo].[tblSynologenShop] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

