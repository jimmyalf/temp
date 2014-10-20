CREATE TABLE [dbo].[tblSynologenContract] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cCode]        NVARCHAR (50)  NULL,
    [cName]        NVARCHAR (50)  NULL,
    [cDescription] NVARCHAR (500) NULL,
    [cAddress]     NVARCHAR (50)  NULL,
    [cAddress2]    NVARCHAR (50)  NULL,
    [cZip]         NVARCHAR (50)  NULL,
    [cCity]        NVARCHAR (50)  NULL,
    [cPhone]       NVARCHAR (50)  NULL,
    [cPhone2]      NVARCHAR (50)  NULL,
    [cFax]         NVARCHAR (50)  NULL,
    [cEmail]       NVARCHAR (50)  NULL,
    [cActive]      BIT            CONSTRAINT [DF_tblSynologenContractCustomer_cActive] DEFAULT (1) NOT NULL,
    CONSTRAINT [PK_tblSynologenContractCustomer] PRIMARY KEY CLUSTERED ([cId] ASC)
);

