ALTER TABLE [dbo].[SynologenOpqNodeSupplierConnections]
    ADD CONSTRAINT [tblBaseUsers_SynologenOpqNodeSupplierConnections_FK1] FOREIGN KEY ([SupId]) REFERENCES [dbo].[tblBaseUsers] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

