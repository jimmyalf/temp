CREATE TABLE [dbo].[tblBaseLogTypes] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (50)  NOT NULL,
    [cDesc] NVARCHAR (256) NULL,
    CONSTRAINT [PK_tblBaseLogTypes] PRIMARY KEY CLUSTERED ([cId] ASC)
);

