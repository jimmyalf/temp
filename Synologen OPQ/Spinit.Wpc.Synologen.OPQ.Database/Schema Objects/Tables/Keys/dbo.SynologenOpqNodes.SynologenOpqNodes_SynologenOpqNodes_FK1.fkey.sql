ALTER TABLE [dbo].[SynologenOpqNodes]
    ADD CONSTRAINT [SynologenOpqNodes_SynologenOpqNodes_FK1] FOREIGN KEY ([Parent]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

