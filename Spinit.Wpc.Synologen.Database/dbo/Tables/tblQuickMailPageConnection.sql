CREATE TABLE [dbo].[tblQuickMailPageConnection] (
    [cMlId]  INT NOT NULL,
    [cPgeId] INT NOT NULL,
    CONSTRAINT [PK_tblQuickMailPageConnection] PRIMARY KEY CLUSTERED ([cMlId] ASC, [cPgeId] ASC),
    CONSTRAINT [FK_tblQuickMailPageConnection_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblQuickMailPageConnection_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

