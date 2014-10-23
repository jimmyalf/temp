CREATE TABLE [dbo].[tblSynologenShopMemberConnection] (
    [cSynologenShopId] INT NOT NULL,
    [cMemberId]        INT NOT NULL,
    CONSTRAINT [PK_tblSynologenShopMemberConnection] PRIMARY KEY CLUSTERED ([cSynologenShopId] ASC, [cMemberId] ASC),
    CONSTRAINT [FK_tblSynologenShopMemberConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId]),
    CONSTRAINT [FK_tblSynologenShopMemberConnection_tblSynologenShop] FOREIGN KEY ([cSynologenShopId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);

