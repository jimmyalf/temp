ALTER TABLE [dbo].[tblSynologenShop]
    ADD CONSTRAINT [FK_tblSynologenShop_tblSynologenShop] FOREIGN KEY ([cId]) REFERENCES [dbo].[tblSynologenShop] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

