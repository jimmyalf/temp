CREATE TABLE [dbo].[SynologenOrderTransaction] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [SubscriptionId]   INT            NOT NULL,
    [Amount]           DECIMAL (7, 2) NOT NULL,
    [Type]             INT            NOT NULL,
    [Reason]           INT            NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [SettlementId]     INT            NULL,
    [PendingPaymentId] INT            NULL,
    CONSTRAINT [PK_SynologenOrderTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderTransaction_SynologenOrderSubscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[SynologenOrderSubscription] ([Id]),
    CONSTRAINT [FK_SynologenOrderTransaction_SynologenOrderSubscriptionPendingPayment] FOREIGN KEY ([PendingPaymentId]) REFERENCES [dbo].[SynologenOrderSubscriptionPendingPayment] ([Id]),
    CONSTRAINT [FK_SynologenOrderTransaction_tblSynologenSettlement] FOREIGN KEY ([SettlementId]) REFERENCES [dbo].[tblSynologenSettlement] ([cId])
);

