ALTER TABLE [dbo].[SynologenOpqDocumentHistories]
    ADD CONSTRAINT [SynologenOpqDocuments_SynologenOpqDocumentHistories_FK1] FOREIGN KEY ([Id]) REFERENCES [dbo].[SynologenOpqDocuments] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

