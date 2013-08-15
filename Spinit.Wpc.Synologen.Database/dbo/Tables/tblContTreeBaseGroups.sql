CREATE TABLE [dbo].[tblContTreeBaseGroups] (
    [cTreId]    INT NULL,
    [cBseGrpId] INT NULL,
    CONSTRAINT [FK_tblContTreeBaseGroups_tblBaseGroups] FOREIGN KEY ([cBseGrpId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblContTreeBaseGroups_tblContTree] FOREIGN KEY ([cTreId]) REFERENCES [dbo].[tblContTree] ([cId])
);

