CREATE TABLE [dbo].[tblGalleryLocationConnection] (
    [cLocationId] INT NOT NULL,
    [cGalleryId]  INT NOT NULL,
    CONSTRAINT [PK_tblGalleryLocationConnection] PRIMARY KEY CLUSTERED ([cLocationId] ASC, [cGalleryId] ASC),
    CONSTRAINT [FK_tblGalleryLocationConnection_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblGalleryLocationConnection_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId])
);

