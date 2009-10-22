ALTER TABLE [dbo].[SynologenOpqFileShops]
    ADD CONSTRAINT [SynologenOpqFiles_SynologenOpqFileShops_FK1] FOREIGN KEY ([FleId]) REFERENCES [dbo].[SynologenOpqFiles] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

