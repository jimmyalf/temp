CREATE TABLE [dbo].[tblNewsLocationConnection] (
    [cNewsId]     INT NOT NULL,
    [cLocationId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsLocatonConnection] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cLocationId] ASC),
    CONSTRAINT [FK_tblNewsLocatonConnection_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblNewsLocatonConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

