CREATE TABLE [dbo].[tblBaseGroupsLanguages] (
    [cGroupId]    INT NOT NULL,
    [cLanguageId] INT NOT NULL,
    [cLocationId] INT NOT NULL,
    CONSTRAINT [PK_tblBaseGroupsLanguages] PRIMARY KEY CLUSTERED ([cGroupId] ASC, [cLanguageId] ASC, [cLocationId] ASC),
    CONSTRAINT [FK_tblBaseGroupsLanguages_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblBaseGroupsLanguages_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblBaseGroupsLanguages_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId])
);

