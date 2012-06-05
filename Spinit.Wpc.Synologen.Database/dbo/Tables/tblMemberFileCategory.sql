CREATE TABLE [dbo].[tblMemberFileCategory] (
    [cId]          INT           IDENTITY (1, 1) NOT NULL,
    [cDescription] NVARCHAR (50) NULL,
    CONSTRAINT [PK_tblMemberFileCategory] PRIMARY KEY CLUSTERED ([cId] ASC)
);

