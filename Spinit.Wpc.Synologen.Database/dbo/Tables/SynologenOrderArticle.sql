CREATE TABLE [dbo].[SynologenOrderArticle] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ArticleTypeId]      INT            NOT NULL,
    [ArticleSupplierId]  INT            NOT NULL,
    [Name]               NVARCHAR (100) NOT NULL,
    [BaseCurveIncrement] DECIMAL (5, 2) NOT NULL,
    [BaseCurveMax]       DECIMAL (5, 2) NOT NULL,
    [BaseCurveMin]       DECIMAL (5, 2) NOT NULL,
    [DiameterIncrement]  DECIMAL (5, 2) NOT NULL,
    [DiameterMax]        DECIMAL (5, 2) NOT NULL,
    [DiameterMin]        DECIMAL (5, 2) NOT NULL,
    [EnableAddition]     BIT            NOT NULL,
    [EnableAxis]         BIT            NOT NULL,
    [EnableCylinder]     BIT            NOT NULL,
    [Active]             BIT            NOT NULL,
    CONSTRAINT [PK_SynologenOrderArticle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderArticle_SynologenOrderArticleSupplier] FOREIGN KEY ([ArticleSupplierId]) REFERENCES [dbo].[SynologenOrderArticleSupplier] ([Id]),
    CONSTRAINT [FK_SynologenOrderArticle_SynologenOrderArticleType] FOREIGN KEY ([ArticleTypeId]) REFERENCES [dbo].[SynologenOrderArticleType] ([Id])
);

