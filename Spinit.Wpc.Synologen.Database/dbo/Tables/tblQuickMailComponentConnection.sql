CREATE TABLE [dbo].[tblQuickMailComponentConnection] (
    [cCmpId] INT NOT NULL,
    [cMlId]  INT NOT NULL,
    CONSTRAINT [PK_tblQuickMailComponentConnection] PRIMARY KEY CLUSTERED ([cCmpId] ASC, [cMlId] ASC),
    CONSTRAINT [FK_tblQuickMailComponentConnection_tblBaseComponents] FOREIGN KEY ([cCmpId]) REFERENCES [dbo].[tblBaseComponents] ([cId]),
    CONSTRAINT [FK_tblQuickMailComponentConnection_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

