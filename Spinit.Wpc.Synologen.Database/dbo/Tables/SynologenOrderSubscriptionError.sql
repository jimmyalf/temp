CREATE TABLE [dbo].[SynologenOrderSubscriptionError] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [SubscriptionId] INT      NOT NULL,
    [Type]           INT      NOT NULL,
    [Code]           INT      NULL,
    [CreatedDate]    DATETIME NOT NULL,
    [HandledDate]    DATETIME NULL,
    [BGConsentId]    INT      NULL,
    [BGPaymentId]    INT      NULL,
    [BGErrorId]      INT      NULL,
    CONSTRAINT [PK_OrderSubscriptionError] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderSubscriptionError_SynologenOrderSubscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[SynologenOrderSubscription] ([Id])
);

