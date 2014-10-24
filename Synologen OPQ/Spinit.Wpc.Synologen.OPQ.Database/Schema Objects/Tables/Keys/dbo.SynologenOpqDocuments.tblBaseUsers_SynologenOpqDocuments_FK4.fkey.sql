ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

