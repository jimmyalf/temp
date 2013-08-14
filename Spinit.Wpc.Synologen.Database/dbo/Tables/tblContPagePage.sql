CREATE TABLE [dbo].[tblContPagePage] (
    [cPgeId] INT NOT NULL,
    [cLnkId] INT NOT NULL,
    CONSTRAINT [PK_tblContPagePage] PRIMARY KEY CLUSTERED ([cPgeId] ASC, [cLnkId] ASC),
    CONSTRAINT [FK_tblContPagePage_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblContPagePage_tblContPage1] FOREIGN KEY ([cLnkId]) REFERENCES [dbo].[tblContPage] ([cId])
);

