CREATE TABLE [dbo].[tblGalleryFiles] (
    [cGalleryId] INT NOT NULL,
    [cFileId]    INT NOT NULL,
    [cOrder]     INT NULL,
    [cVisible]   BIT CONSTRAINT [DF_tblGalleryFiles_cVisible] DEFAULT (1) NULL,
    CONSTRAINT [PK_tblGalleryFiles] PRIMARY KEY CLUSTERED ([cGalleryId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblGalleryFiles_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblGalleryFiles_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId])
);

