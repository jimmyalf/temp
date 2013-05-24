CREATE TABLE [dbo].[tblMemberLanguageConnection] (
    [cLanguageId]       INT NULL,
    [cMembersContentId] INT NULL,
    CONSTRAINT [FK_tblMemberLanguageConnection_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblMemberLanguageConnection_tblMembersContent] FOREIGN KEY ([cMembersContentId]) REFERENCES [dbo].[tblMembersContent] ([cId])
);

