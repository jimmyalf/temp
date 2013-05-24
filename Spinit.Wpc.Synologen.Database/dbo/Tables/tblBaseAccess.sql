CREATE TABLE [dbo].[tblBaseAccess] (
    [cId]     INT            IDENTITY (1, 1) NOT NULL,
    [cName]   NVARCHAR (256) NOT NULL,
    [cAccess] INT            NOT NULL,
    CONSTRAINT [PK_tblBaseAccess] PRIMARY KEY CLUSTERED ([cId] ASC)
);

