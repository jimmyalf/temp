CREATE TABLE [dbo].[tblSynologenSettlement] (
    [cId]          INT           IDENTITY (1, 1) NOT NULL,
    [cCreatedDate] SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_tblSynologenSettlement] PRIMARY KEY CLUSTERED ([cId] ASC)
);

