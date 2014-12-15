ALTER TABLE [dbo].[tblContPage]
    ADD CONSTRAINT [FK_tblContPage_tblContPageType] FOREIGN KEY ([cPgeTpeId]) REFERENCES [dbo].[tblContPageType] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

