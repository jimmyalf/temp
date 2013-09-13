CREATE TABLE [dbo].[tblGalleryFileTypeConnection] (
    [cGalleryId]  INT NOT NULL,
    [cFileTypeId] INT NOT NULL,
    CONSTRAINT [PK_tblGalleryFileTypeConnection] PRIMARY KEY CLUSTERED ([cGalleryId] ASC, [cFileTypeId] ASC),
    CONSTRAINT [FK_tblGalleryFileTypeConnection_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId]),
    CONSTRAINT [FK_tblGalleryFileTypeConnection_tblGalleryFileType] FOREIGN KEY ([cFileTypeId]) REFERENCES [dbo].[tblGalleryFileType] ([cId])
);

