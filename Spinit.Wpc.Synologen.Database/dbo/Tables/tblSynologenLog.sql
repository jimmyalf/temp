CREATE TABLE [dbo].[tblSynologenLog] (
    [cId]          INT             IDENTITY (1, 1) NOT NULL,
    [cMessage]     NVARCHAR (2000) NULL,
    [cMessageType] INT             NOT NULL,
    [cCreatedDate] SMALLDATETIME   CONSTRAINT [DF_tblSynologenLog_cCreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblSynologenLog] PRIMARY KEY CLUSTERED ([cId] ASC)
);

