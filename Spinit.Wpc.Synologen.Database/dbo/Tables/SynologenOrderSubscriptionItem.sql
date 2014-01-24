CREATE TABLE [dbo].[SynologenOrderSubscriptionItem] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [WithdrawalsLimit]     INT            NOT NULL,
    [PerformedWithdrawals] INT            NOT NULL,
    [SubscriptionId]       INT            NOT NULL,
    [ProductPrice]         DECIMAL (7, 2) NOT NULL,
    [FeePrice]             DECIMAL (7, 2) NOT NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    CONSTRAINT [PK_OrderSubscriptionItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderSubscriptionItem_SynologenOrderSubscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[SynologenOrderSubscription] ([Id])
);

