CREATE TABLE [dbo].[tblMemberLanguageStrings] (
    [cId]     INT            NOT NULL,
    [cLngId]  INT            NOT NULL,
    [cString] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblMemberLanguageStrings] PRIMARY KEY CLUSTERED ([cId] ASC, [cLngId] ASC)
);

