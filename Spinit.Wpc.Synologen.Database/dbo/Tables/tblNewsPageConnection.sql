CREATE TABLE [dbo].[tblNewsPageConnection] (
    [cNewsId] INT NOT NULL,
    [cPageId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsPage] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cPageId] ASC),
    CONSTRAINT [FK_tblNewsPageConnection_tblContPage] FOREIGN KEY ([cPageId]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblNewsPageConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

