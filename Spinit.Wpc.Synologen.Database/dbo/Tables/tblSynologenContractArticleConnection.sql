CREATE TABLE [dbo].[tblSynologenContractArticleConnection] (
    [cId]                        INT           IDENTITY (1, 1) NOT NULL,
    [cContractCustomerId]        INT           NOT NULL,
    [cArticleId]                 INT           NOT NULL,
    [cPrice]                     FLOAT (53)    NOT NULL,
    [cNoVat]                     BIT           CONSTRAINT [DF_tblSynologenContractArticleConnection_cNoVat] DEFAULT (0) NOT NULL,
    [cActive]                    BIT           NOT NULL,
    [cSPCSAccountNumber]         NVARCHAR (50) CONSTRAINT [DF_tblSynologenContractArticleConnection_cSPCSAccountNumber] DEFAULT (N'3071') NOT NULL,
    [cEnableManualPriceOverride] BIT           CONSTRAINT [DF_tblSynologenContractArticleConnection_cEnableManualPriceOverride] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tblSynologenContractCustomerArticleConnection_1] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblSynologenContractArticleConnection_tblSynologenArticle] FOREIGN KEY ([cArticleId]) REFERENCES [dbo].[tblSynologenArticle] ([cId]),
    CONSTRAINT [FK_tblSynologenContractArticleConnection_tblSynologenContract] FOREIGN KEY ([cContractCustomerId]) REFERENCES [dbo].[tblSynologenContract] ([cId])
);

