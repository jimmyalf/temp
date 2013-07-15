CREATE TABLE [dbo].[tblSynologenOrderHistory] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cOrderId]     INT            NOT NULL,
    [cText]        NVARCHAR (500) NOT NULL,
    [cCreatedDate] SMALLDATETIME  NOT NULL,
    CONSTRAINT [PK_tblSynologenOrderHistory] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblSynologenOrderHistory_tblSynologenOrder] FOREIGN KEY ([cOrderId]) REFERENCES [dbo].[tblSynologenOrder] ([cId])
);

