CREATE TABLE [dbo].[tblGallerySortType] (
    [cId]          INT           IDENTITY (1, 1) NOT NULL,
    [cDescription] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblGallerySortType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

