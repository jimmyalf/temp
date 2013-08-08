CREATE TABLE [dbo].[SynologenOrderSubscription] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ShopId]            INT           NOT NULL,
    [CustomerId]        INT           NOT NULL,
    [Active]            BIT           NOT NULL,
    [AutogiroPayerId]   INT           NULL,
    [BankAccountNumber] NVARCHAR (12) NOT NULL,
    [ClearingNumber]    NVARCHAR (4)  NOT NULL,
    [ConsentStatus]     INT           NOT NULL,
    [CreatedDate]       DATETIME      NOT NULL,
    [ConsentedDate]     DATETIME      NULL,
    [LastPaymentSent]   DATETIME      NULL,
    CONSTRAINT [PK_SynologenOrderSubscription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrderSubscription_SynologenOrderCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[SynologenOrderCustomer] ([Id]),
    CONSTRAINT [FK_SynologenOrderSubscription_tblSynologenShop] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

