CREATE TABLE [dbo].[tblGalleryLanguageConnection] (
    [cLanguageId] INT NOT NULL,
    [cGalleryId]  INT NOT NULL,
    CONSTRAINT [PK_tblGalleryLanguageConnection] PRIMARY KEY CLUSTERED ([cLanguageId] ASC, [cGalleryId] ASC),
    CONSTRAINT [FK_tblGalleryLanguageConnection_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblGalleryLanguageConnection_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId])
);

