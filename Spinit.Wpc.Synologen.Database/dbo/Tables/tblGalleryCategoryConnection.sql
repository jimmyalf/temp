CREATE TABLE [dbo].[tblGalleryCategoryConnection] (
    [cGalleryId]  INT NOT NULL,
    [cCategoryId] INT NOT NULL,
    CONSTRAINT [PK_tblGalleryCategoryConnection] PRIMARY KEY CLUSTERED ([cGalleryId] ASC, [cCategoryId] ASC),
    CONSTRAINT [FK_tblGalleryCategoryConnection_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId]),
    CONSTRAINT [FK_tblGalleryCategoryConnection_tblGalleryCategories] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblGalleryCategories] ([cId])
);

