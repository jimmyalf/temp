ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [SynologenOpqNodes_SynologenOpqDocuments_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

