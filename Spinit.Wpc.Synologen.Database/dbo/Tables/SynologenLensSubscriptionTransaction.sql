CREATE TABLE [dbo].[SynologenLensSubscriptionTransaction] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [SubscriptionId] INT            NOT NULL,
    [Amount]         DECIMAL (7, 2) NOT NULL,
    [Type]           INT            NOT NULL,
    [Reason]         INT            NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [SettlementId]   INT            NULL,
    [ArticleId]      INT            NULL,
    CONSTRAINT [PK_SynologenLensSubscriptionTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenLensSubscriptionTransaction_SynologenLensSubscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[SynologenLensSubscription] ([Id]),
    CONSTRAINT [FK_SynologenLensSubscriptionTransaction_SynologenLensSubscriptionTransactionArticle] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[SynologenLensSubscriptionTransactionArticle] ([Id]),
    CONSTRAINT [FK_SynologenLensSubscriptionTransaction_tblSynologenSettlement] FOREIGN KEY ([SettlementId]) REFERENCES [dbo].[tblSynologenSettlement] ([cId])
);

