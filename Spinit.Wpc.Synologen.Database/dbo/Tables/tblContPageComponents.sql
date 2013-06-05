CREATE TABLE [dbo].[tblContPageComponents] (
    [cPgeId] INT NOT NULL,
    [cCmpId] INT NOT NULL,
    CONSTRAINT [PK_tblPageWPCComponents] PRIMARY KEY CLUSTERED ([cPgeId] ASC, [cCmpId] ASC),
    CONSTRAINT [FK_tblContPageComponents_tblBaseComponents] FOREIGN KEY ([cCmpId]) REFERENCES [dbo].[tblBaseComponents] ([cId]),
    CONSTRAINT [FK_tblContPageComponents_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId])
);

