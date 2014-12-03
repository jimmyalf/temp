CREATE TABLE [dbo].[tblSynologenConcern] (
    [cId]        INT            IDENTITY (1, 1) NOT NULL,
    [cName]      NVARCHAR (512) NOT NULL,
    [cCommonOpq] BIT            NULL,
    CONSTRAINT [tblSynologenConcern_PK] PRIMARY KEY CLUSTERED ([cId] ASC)
);

