ALTER TABLE [dbo].[tblContPageFile]
    ADD CONSTRAINT [FK_tblContPageFile_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

