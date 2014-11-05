CREATE TABLE [dbo].[tblGalleryGroupConnection] (
    [cGalleryId] INT NOT NULL,
    [cGroupId]   INT NOT NULL,
    CONSTRAINT [PK_tblGalleryGroupConnection] PRIMARY KEY CLUSTERED ([cGalleryId] ASC, [cGroupId] ASC),
    CONSTRAINT [FK_tblGalleryGroupConnection_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblGalleryGroupConnection_tblGallery] FOREIGN KEY ([cGalleryId]) REFERENCES [dbo].[tblGallery] ([cId])
);

