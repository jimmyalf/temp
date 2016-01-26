CREATE TABLE [dbo].[SynologenOrderArticleCategory] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [Active] BIT           NOT NULL,
    CONSTRAINT [PK_SynologenOrderArticleCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

