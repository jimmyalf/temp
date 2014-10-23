CREATE TABLE [dbo].[tblGalleryFileType] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cDescription] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblGalleryFileType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

