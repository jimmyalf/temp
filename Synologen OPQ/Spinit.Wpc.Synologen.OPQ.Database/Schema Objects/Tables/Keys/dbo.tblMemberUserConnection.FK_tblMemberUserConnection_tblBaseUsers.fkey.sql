ALTER TABLE [dbo].[tblMemberUserConnection]
    ADD CONSTRAINT [FK_tblMemberUserConnection_tblBaseUsers] FOREIGN KEY ([cUserId]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

