ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

