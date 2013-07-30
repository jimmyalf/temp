ALTER TABLE [dbo].[SynologenOpqDocuments]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqDocuments_FK3] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

