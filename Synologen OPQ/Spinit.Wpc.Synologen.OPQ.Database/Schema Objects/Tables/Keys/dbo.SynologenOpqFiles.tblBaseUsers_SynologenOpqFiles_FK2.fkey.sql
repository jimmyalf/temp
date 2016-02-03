ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

