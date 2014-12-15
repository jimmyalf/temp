ALTER TABLE [dbo].[SynologenOpqNodes]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

