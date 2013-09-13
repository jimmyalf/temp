CREATE TABLE [dbo].[SynologenOrder] (
    [Id]                          INT            IDENTITY (1, 1) NOT NULL,
    [ShopId]                      INT            NOT NULL,
    [CustomerId]                  INT            NOT NULL,
    [LensRecipeId]                INT            NULL,
    [PaymentOptionSubscripitonId] INT            NULL,
    [PaymentOptionType]           INT            NULL,
    [SubscriptionItemId]          INT            NULL,
    [Created]                     DATETIME       NULL,
    [ShippingType]                INT            NULL,
    [OrderTotalWithdrawalAmount]  DECIMAL (7, 2) NOT NULL,
    [SpinitServicesEmailId]       INT            NULL,
    [Status]                      INT            NOT NULL,
    [Reference]                   NVARCHAR (255) NULL,
    CONSTRAINT [PK_SynologenOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOrder_SynologenOrderCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[SynologenOrderCustomer] ([Id]),
    CONSTRAINT [FK_SynologenOrder_SynologenOrderLensRecipe] FOREIGN KEY ([LensRecipeId]) REFERENCES [dbo].[SynologenOrderLensRecipe] ([Id]),
    CONSTRAINT [FK_SynologenOrder_SynologenOrderSubscription] FOREIGN KEY ([PaymentOptionSubscripitonId]) REFERENCES [dbo].[SynologenOrderSubscription] ([Id]),
    CONSTRAINT [FK_SynologenOrder_SynologenOrderSubscriptionItem] FOREIGN KEY ([SubscriptionItemId]) REFERENCES [dbo].[SynologenOrderSubscriptionItem] ([Id]),
    CONSTRAINT [FK_SynologenOrder_tblSynologenShop] FOREIGN KEY ([ShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

