ALTER TABLE [dbo].[SynologenOpqFiles]
    ADD CONSTRAINT [SynologenOpqNodes_SynologenOpqFiles_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

