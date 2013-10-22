ALTER TABLE [dbo].[tblSynologenShopMemberConnection]
    ADD CONSTRAINT [FK_tblSynologenShopMemberConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

