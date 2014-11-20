CREATE TABLE [dbo].[SynologenLensSubscriptionError] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [SubscriptionId] INT      NOT NULL,
    [Type]           INT      NOT NULL,
    [Code]           INT      NULL,
    [CreatedDate]    DATETIME NOT NULL,
    [HandledDate]    DATETIME NULL,
    [IsHandled]      BIT      NULL,
    [BGConsentId]    INT      NULL,
    [BGPaymentId]    INT      NULL,
    [BGErrorId]      INT      NULL,
    CONSTRAINT [PK_SubscriptionError] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenLensSubscriptionError_SynologenLensSubscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[SynologenLensSubscription] ([Id])
);

