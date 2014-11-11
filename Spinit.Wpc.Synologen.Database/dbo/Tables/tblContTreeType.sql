CREATE TABLE [dbo].[tblContTreeType] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NOT NULL,
    [cDescription] NVARCHAR (256) NULL,
    CONSTRAINT [PK_tblContTreeType] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [IX_tblContTreeType] UNIQUE NONCLUSTERED ([cName] ASC)
);

