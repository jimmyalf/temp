CREATE TABLE [dbo].[tblCommerceConfiguration] (
    [cId]    INT             NOT NULL,
    [cName]  NVARCHAR (512)  NOT NULL,
    [cValue] NVARCHAR (1024) NOT NULL,
    CONSTRAINT [PK_tblCommerceConfiguration] PRIMARY KEY CLUSTERED ([cId] ASC)
);

