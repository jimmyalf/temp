ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

