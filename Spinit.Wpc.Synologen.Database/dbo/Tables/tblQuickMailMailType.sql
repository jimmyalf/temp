CREATE TABLE [dbo].[tblQuickMailMailType] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_tblQuickMailMailType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

