CREATE TABLE [dbo].[tblBaseLocationsComponents] (
    [cLocationId]  INT NOT NULL,
    [cComponentId] INT NOT NULL,
    CONSTRAINT [PK_tblBaseLanguagesComponents] PRIMARY KEY CLUSTERED ([cLocationId] ASC, [cComponentId] ASC),
    CONSTRAINT [FK_tblBaseLocationsComponents_tblBaseComponents] FOREIGN KEY ([cComponentId]) REFERENCES [dbo].[tblBaseComponents] ([cId]),
    CONSTRAINT [FK_tblBaseLocationsComponents_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId])
);

