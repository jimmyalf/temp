CREATE TABLE [dbo].[tblSynologenRst] (
    [cId]        INT           IDENTITY (1, 1) NOT NULL,
    [cCompanyId] INT           NOT NULL,
    [cName]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblSynologenRst] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblSynologenRst_tblSynologenCompany] FOREIGN KEY ([cCompanyId]) REFERENCES [dbo].[tblSynologenCompany] ([cId])
);

