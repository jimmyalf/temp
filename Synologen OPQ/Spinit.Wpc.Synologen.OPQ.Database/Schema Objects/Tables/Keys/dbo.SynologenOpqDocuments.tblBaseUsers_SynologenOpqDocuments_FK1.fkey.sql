ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

