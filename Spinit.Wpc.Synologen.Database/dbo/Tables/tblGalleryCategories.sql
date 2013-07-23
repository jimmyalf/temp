CREATE TABLE [dbo].[tblGalleryCategories] (
    [cId]           INT IDENTITY (1, 1) NOT NULL,
    [cNameStringId] INT NULL,
    [cOrderId]      INT NULL,
    CONSTRAINT [PK_tblGalleryCategories] PRIMARY KEY CLUSTERED ([cId] ASC)
);

