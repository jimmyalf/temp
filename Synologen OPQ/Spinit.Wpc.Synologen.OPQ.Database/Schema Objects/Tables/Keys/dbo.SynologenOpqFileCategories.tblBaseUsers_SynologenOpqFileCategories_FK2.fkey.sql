ALTER TABLE [dbo].[SynologenOpqFileCategories]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqFileCategories_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

