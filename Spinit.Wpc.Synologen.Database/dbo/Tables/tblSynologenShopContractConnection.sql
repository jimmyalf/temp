CREATE TABLE [dbo].[tblSynologenShopContractConnection] (
    [cSynologenShopId]             INT NOT NULL,
    [cSynologenContractCustomerId] INT NOT NULL,
    CONSTRAINT [PK_tblSynologenShopContractConnection] PRIMARY KEY CLUSTERED ([cSynologenShopId] ASC, [cSynologenContractCustomerId] ASC),
    CONSTRAINT [FK_tblSYnologenShopContractCustomerConnection_tblSynologenContractCustomer] FOREIGN KEY ([cSynologenContractCustomerId]) REFERENCES [dbo].[tblSynologenContract] ([cId]),
    CONSTRAINT [FK_tblSYnologenShopContractCustomerConnection_tblSynologenShop] FOREIGN KEY ([cSynologenShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

