CREATE TABLE [dbo].[tblNewsLanguageConnection] (
    [cNewsId]     INT NOT NULL,
    [cLanguageId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsLanguageConnection] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cLanguageId] ASC),
    CONSTRAINT [FK_tblNewsLanguageConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

