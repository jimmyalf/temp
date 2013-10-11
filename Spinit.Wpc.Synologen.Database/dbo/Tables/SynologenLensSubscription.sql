CREATE TABLE [dbo].[SynologenLensSubscription] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [CustomerId]          INT             NOT NULL,
    [MonthlyAmount]       DECIMAL (7, 2)  NOT NULL,
    [AccountNumber]       NVARCHAR (12)   NOT NULL,
    [ClearingNumber]      NVARCHAR (4)    NOT NULL,
    [Active]              BIT             CONSTRAINT [DF_SynologenLensSubscription_Active] DEFAULT (0) NOT NULL,
    [Status]              INT             NULL,
    [CreatedDate]         DATETIME        NOT NULL,
    [ActivatedDate]       DATETIME        NULL,
    [ConsentDate]         DATETIME        NULL,
    [Notes]               NVARCHAR (4000) NULL,
    [ConsentStatus]       INT             CONSTRAINT [DF_SynologenLensSubscription_ConsentStatus] DEFAULT (0) NOT NULL,
    [PaymentSentDate]     DATETIME        NULL,
    [BankgiroPayerNumber] INT             NULL,
    CONSTRAINT [PK_SynologenLensSubscription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenLensSubscription_SynologenLensSubscriptionCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[SynologenLensSubscriptionCustomer] ([Id])
);

