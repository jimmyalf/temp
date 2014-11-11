CREATE TABLE [dbo].[SynologenOpqDocumentTypes] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (512) NOT NULL,
    CONSTRAINT [SynologenOpqDocumentTypes_PK] PRIMARY KEY CLUSTERED ([Id] ASC)
);

