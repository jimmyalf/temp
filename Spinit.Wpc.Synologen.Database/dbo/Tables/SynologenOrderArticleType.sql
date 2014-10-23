CREATE TABLE [dbo].[SynologenOrderArticleType] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NOT NULL,
    [ArticleCategoryId] INT           NULL,
    [Active]            BIT           NOT NULL,
    CONSTRAINT [PK_SynologenOrderArticleType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderArticleType_SynologenOrderArticleCategory] FOREIGN KEY ([ArticleCategoryId]) REFERENCES [dbo].[SynologenOrderArticleCategory] ([Id])
);

