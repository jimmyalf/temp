ALTER TABLE [dbo].[SynologenOpqDocumentHistories]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqDocumentHistories_FK1] FOREIGN KEY ([HistoryId]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

