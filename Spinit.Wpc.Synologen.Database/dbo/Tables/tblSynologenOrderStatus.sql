CREATE TABLE [dbo].[tblSynologenOrderStatus] (
    [cId]    INT           IDENTITY (1, 1) NOT NULL,
    [cOrder] INT           NOT NULL,
    [cName]  NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblSynologenOrderStatus] PRIMARY KEY CLUSTERED ([cId] ASC)
);

