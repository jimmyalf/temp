ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [SynologenOpqDocumentTypes_SynologenOpqDocuments_FK1] FOREIGN KEY ([DocTpeId]) REFERENCES [dbo].[SynologenOpqDocumentTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

