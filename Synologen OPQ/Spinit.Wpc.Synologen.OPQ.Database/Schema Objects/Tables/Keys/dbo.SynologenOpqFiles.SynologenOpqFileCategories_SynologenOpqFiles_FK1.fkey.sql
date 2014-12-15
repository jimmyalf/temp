ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [SynologenOpqFileCategories_SynologenOpqFiles_FK1] FOREIGN KEY ([FleCatId]) REFERENCES [dbo].[SynologenOpqFileCategories] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

