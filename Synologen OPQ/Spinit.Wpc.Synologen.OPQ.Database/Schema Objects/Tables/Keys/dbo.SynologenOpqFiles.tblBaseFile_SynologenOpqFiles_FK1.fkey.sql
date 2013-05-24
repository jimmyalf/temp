ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [tblBaseFile_SynologenOpqFiles_FK1] FOREIGN KEY ([FleId]) REFERENCES [dbo].[tblBaseFile] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

