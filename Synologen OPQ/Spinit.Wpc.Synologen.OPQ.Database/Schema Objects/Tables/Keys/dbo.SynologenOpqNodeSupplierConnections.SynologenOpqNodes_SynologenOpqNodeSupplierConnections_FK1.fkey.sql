ALTER TABLE [dbo].[SynologenOpqNodeSupplierConnections]
    ADD CONSTRAINT [SynologenOpqNodes_SynologenOpqNodeSupplierConnections_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

