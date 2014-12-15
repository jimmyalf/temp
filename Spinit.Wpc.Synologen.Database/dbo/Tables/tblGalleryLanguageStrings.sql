CREATE TABLE [dbo].[tblGalleryLanguageStrings] (
    [cId]     INT            NOT NULL,
    [cLngId]  INT            NOT NULL,
    [cString] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblGalleryLanguageStrings] PRIMARY KEY CLUSTERED ([cId] ASC, [cLngId] ASC)
);

