ALTER TABLE [dbo].[SynologenOpqNodes]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

