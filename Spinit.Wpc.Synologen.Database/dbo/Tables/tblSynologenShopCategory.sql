﻿CREATE TABLE [dbo].[tblSynologenShopCategory] (
    [cId]   INT           IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblSynologenShopCategory] PRIMARY KEY CLUSTERED ([cId] ASC)
);

