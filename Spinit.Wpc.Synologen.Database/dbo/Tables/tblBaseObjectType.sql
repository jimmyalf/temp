CREATE TABLE [dbo].[tblBaseObjectType] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NULL,
    [cDescription] NVARCHAR (512) NULL,
    CONSTRAINT [PK_tblBaseObjectType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

