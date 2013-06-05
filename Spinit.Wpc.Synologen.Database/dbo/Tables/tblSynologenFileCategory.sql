CREATE TABLE [dbo].[tblSynologenFileCategory] (
    [cId]          INT           IDENTITY (1, 1) NOT NULL,
    [cDescription] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblSynologenFileCategory] PRIMARY KEY CLUSTERED ([cId] ASC)
);

