ALTER TABLE [dbo].[tblMemberUserConnection]
    ADD CONSTRAINT [FK_tblMemberUserConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

