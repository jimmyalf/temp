CREATE TABLE [dbo].[tblQuickMailLanguageConnection] (
    [cLngId] INT NOT NULL,
    [cMlId]  INT NOT NULL,
    CONSTRAINT [PK_tblQuickMailLanguageConnection] PRIMARY KEY CLUSTERED ([cLngId] ASC, [cMlId] ASC),
    CONSTRAINT [FK_tblQuickMailLanguageConnection_tblBaseLanguages] FOREIGN KEY ([cLngId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblQuickMailLanguageConnection_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

