CREATE TABLE [dbo].[tblForumstatistics_User] (
    [UserID]     INT CONSTRAINT [DF_forums_statistics_User_UserID] DEFAULT (0) NOT NULL,
    [TotalPosts] INT CONSTRAINT [DF_forums_MostActiveUsers_TotalPosts] DEFAULT (0) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_MostActiveUsers]
    ON [dbo].[tblForumstatistics_User]([TotalPosts] DESC);

