CREATE TABLE [dbo].[tblContPageFile] (
    [cPgeId] INT NOT NULL,
    [cFleId] INT NOT NULL,
    CONSTRAINT [PK_tblContPageFile] PRIMARY KEY CLUSTERED ([cPgeId] ASC, [cFleId] ASC),
    CONSTRAINT [FK_tblContPageFile_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblContPageFile_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId])
);

