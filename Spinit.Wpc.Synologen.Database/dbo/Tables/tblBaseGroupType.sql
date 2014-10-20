CREATE TABLE [dbo].[tblBaseGroupType] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NOT NULL,
    [cDescription] NVARCHAR (512) NULL,
    CONSTRAINT [PK_tblBaseGroupType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

