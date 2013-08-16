ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblSynologenConcern_SynologenOpqDocuments_FK1] FOREIGN KEY ([CncId]) REFERENCES [dbo].[tblSynologenConcern] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

