CREATE TABLE [dbo].[tblForumSearchBarrel] (
    [WordHash] INT           NOT NULL,
    [Word]     NVARCHAR (64) CONSTRAINT [DF_forums_SearchBarrel_word] DEFAULT ('') NOT NULL,
    [PostID]   INT           NOT NULL,
    [ThreadID] INT           CONSTRAINT [DF_forums_SearchBarrel_threadId_1] DEFAULT (0) NOT NULL,
    [ForumID]  INT           CONSTRAINT [DF_forums_SearchBarrel_forumId] DEFAULT (0) NOT NULL,
    [Weight]   FLOAT (53)    CONSTRAINT [DF_forums_SearchBarrel_weight] DEFAULT (0) NOT NULL,
    CONSTRAINT [FK_forums_SearchBarrel_forums_Posts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[tblForumPosts] ([PostID]) ON DELETE CASCADE NOT FOR REPLICATION,
    CONSTRAINT [FK_forums_SearchBarrel_forums_Threads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[tblForumThreads] ([ThreadID]) ON DELETE CASCADE NOT FOR REPLICATION,
    CONSTRAINT [IX_forums_SearchBarrel] UNIQUE NONCLUSTERED ([WordHash] ASC, [PostID] ASC)
);


GO
CREATE CLUSTERED INDEX [IX_forums_SearchBarrel_1]
    ON [dbo].[tblForumSearchBarrel]([WordHash] ASC, [PostID] ASC, [Weight] DESC);

