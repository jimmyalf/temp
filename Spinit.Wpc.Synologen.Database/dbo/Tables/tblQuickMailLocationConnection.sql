CREATE TABLE [dbo].[tblQuickMailLocationConnection] (
    [cLocId] INT NOT NULL,
    [cMlId]  INT NOT NULL,
    CONSTRAINT [PK_tblQuickMailLocationConnection] PRIMARY KEY CLUSTERED ([cMlId] ASC, [cLocId] ASC),
    CONSTRAINT [FK_tblQuickMailLocationConnection_tblBaseLocations] FOREIGN KEY ([cLocId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblQuickMailLocationConnection_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

