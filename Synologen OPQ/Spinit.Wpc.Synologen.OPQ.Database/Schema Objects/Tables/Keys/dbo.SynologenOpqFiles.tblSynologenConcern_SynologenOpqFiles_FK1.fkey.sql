ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblSynologenConcern_SynologenOpqFiles_FK1] FOREIGN KEY ([CncId]) REFERENCES [dbo].[tblSynologenConcern] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

