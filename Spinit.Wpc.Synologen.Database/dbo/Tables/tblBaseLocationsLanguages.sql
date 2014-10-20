CREATE TABLE [dbo].[tblBaseLocationsLanguages] (
    [cLocationId] INT NOT NULL,
    [cLanguageId] INT NOT NULL,
    [cIsDefault]  BIT DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tblBaseLocationsLanguages] PRIMARY KEY CLUSTERED ([cLocationId] ASC, [cLanguageId] ASC),
    CONSTRAINT [FK_tblBaseLocationsLanguages_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblBaseLocationsLanguages_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId])
);

