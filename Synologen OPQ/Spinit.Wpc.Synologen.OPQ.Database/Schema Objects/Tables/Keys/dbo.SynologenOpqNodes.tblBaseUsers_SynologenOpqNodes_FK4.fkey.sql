ALTER TABLE [dbo].[SynologenOpqNodes]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

