CREATE TABLE [dbo].[SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem] (
    [SubscriptionItemId]           INT NOT NULL,
    [SubscriptionPendingPaymentId] INT NOT NULL,
    CONSTRAINT [PK_SynologenSubscriptionPendingPayment_SynologenSubscriptionItem] PRIMARY KEY CLUSTERED ([SubscriptionItemId] ASC, [SubscriptionPendingPaymentId] ASC),
    CONSTRAINT [FK_SynologenSubscriptionPendingPayment_SynologenSubscriptionItem_SynologenOrderSubscriptionItem] FOREIGN KEY ([SubscriptionItemId]) REFERENCES [dbo].[SynologenOrderSubscriptionItem] ([Id]),
    CONSTRAINT [FK_SynologenSubscriptionPendingPayment_SynologenSubscriptionItem_SynologenSubscriptionPendingPayment] FOREIGN KEY ([SubscriptionPendingPaymentId]) REFERENCES [dbo].[SynologenOrderSubscriptionPendingPayment] ([Id])
);

