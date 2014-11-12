ALTER TABLE [dbo].[tblContPageFile]
    ADD CONSTRAINT [FK_tblContPageFile_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



