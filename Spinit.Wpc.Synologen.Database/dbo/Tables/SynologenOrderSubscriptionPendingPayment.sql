CREATE TABLE [dbo].[SynologenOrderSubscriptionPendingPayment] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [TaxedAmount]   DECIMAL (7, 2) NOT NULL,
    [TaxFreeAmount] DECIMAL (7, 2) NOT NULL,
    [Created]       DATETIME       NOT NULL,
    [HasBeenPayed]  BIT            NOT NULL,
    CONSTRAINT [PK_SynologenSubscriptionPendingPaymentMap] PRIMARY KEY CLUSTERED ([Id] ASC)
);

