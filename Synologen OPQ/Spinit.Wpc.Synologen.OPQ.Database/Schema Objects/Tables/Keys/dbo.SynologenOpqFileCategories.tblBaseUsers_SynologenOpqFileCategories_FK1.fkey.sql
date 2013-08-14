ALTER TABLE [dbo].[SynologenOpqFileCategories]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqFileCategories_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

