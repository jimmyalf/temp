CREATE TABLE [dbo].[SynologenLensSubscriptionTransactionArticle] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (255) NOT NULL,
    [Active] BIT            NOT NULL,
    CONSTRAINT [PK_SynologenLensSubscriptionTransactionArticle] PRIMARY KEY CLUSTERED ([Id] ASC)
);

