CREATE TABLE [dbo].[tblBaseGroupsLocations] (
    [cGroupId]    INT NOT NULL,
    [cLocationId] INT NOT NULL,
    CONSTRAINT [PK_tblBaseGroupsLocations] PRIMARY KEY CLUSTERED ([cGroupId] ASC, [cLocationId] ASC),
    CONSTRAINT [FK_tblBaseGroupsLocations_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblBaseGroupsLocations_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId])
);

