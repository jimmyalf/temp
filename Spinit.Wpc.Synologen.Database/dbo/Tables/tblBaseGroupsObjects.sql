CREATE TABLE [dbo].[tblBaseGroupsObjects] (
    [cGroupId]  INT NOT NULL,
    [cObjectId] INT NOT NULL,
    [cObjTpeId] INT NOT NULL,
    CONSTRAINT [PK_tblBaseGroupsObjects] PRIMARY KEY CLUSTERED ([cGroupId] ASC, [cObjectId] ASC),
    CONSTRAINT [FK_tblBaseGroupsObjects_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblBaseGroupsObjects_tblBaseObjects] FOREIGN KEY ([cObjectId]) REFERENCES [dbo].[tblBaseObjects] ([cId]),
    CONSTRAINT [FK_tblBaseGroupsObjects_tblBaseObjectType] FOREIGN KEY ([cObjTpeId]) REFERENCES [dbo].[tblBaseObjectType] ([cId])
);

