CREATE TABLE [dbo].[tblSynologenGiro] (
    [cId]        INT           IDENTITY (1, 1) NOT NULL,
    [cName]      NVARCHAR (50) NOT NULL,
    [cShortName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_tblSynologenGiro] PRIMARY KEY CLUSTERED ([cId] ASC)
);

