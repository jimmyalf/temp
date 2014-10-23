CREATE TABLE [dbo].[tblNewsGroupConnection] (
    [cNewsId]  INT NOT NULL,
    [cGroupId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsGroupConnection] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cGroupId] ASC),
    CONSTRAINT [FK_tblNewsGroupConnection_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblNewsGroupConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

