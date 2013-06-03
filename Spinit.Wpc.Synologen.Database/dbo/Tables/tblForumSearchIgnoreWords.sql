CREATE TABLE [dbo].[tblForumSearchIgnoreWords] (
    [WordHash] INT            NOT NULL,
    [Word]     NVARCHAR (256) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_SearchWordDictionary]
    ON [dbo].[tblForumSearchIgnoreWords]([WordHash] ASC);

