CREATE TABLE [dbo].[tblQuickMailFileConnection] (
    [cMlId]  INT NOT NULL,
    [cFleId] INT NOT NULL,
    CONSTRAINT [PK_tblQuickMailFileConnection] PRIMARY KEY CLUSTERED ([cMlId] ASC, [cFleId] ASC),
    CONSTRAINT [FK_tblQuickMailFileConnection_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblQuickMailFileConnection_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

