CREATE TABLE [dbo].[tblSynologenSettlementOrderConnection] (
    [cSettlementId] INT NOT NULL,
    [cOrderId]      INT NOT NULL,
    CONSTRAINT [PK_tblSynologenSettlementOrderConnection] PRIMARY KEY CLUSTERED ([cSettlementId] ASC, [cOrderId] ASC),
    CONSTRAINT [FK_tblSynologenSettlementOrderConnection_tblSynologenOrder] FOREIGN KEY ([cOrderId]) REFERENCES [dbo].[tblSynologenOrder] ([cId]),
    CONSTRAINT [FK_tblSynologenSettlementOrderConnection_tblSynologenSettlement] FOREIGN KEY ([cSettlementId]) REFERENCES [dbo].[tblSynologenSettlement] ([cId])
);

