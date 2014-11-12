ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

