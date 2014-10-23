CREATE TABLE [dbo].[tblForumThreadsRead] (
    [UserID]       INT NOT NULL,
    [ForumGroupID] INT CONSTRAINT [DF_forums_ThreadsRead_ForumGroupID] DEFAULT (0) NOT NULL,
    [ForumID]      INT CONSTRAINT [DF_forums_ThreadsRead_ForumID] DEFAULT (0) NOT NULL,
    [ThreadID]     INT NOT NULL,
    CONSTRAINT [FK_forums_ThreadsRead_forums_Threads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[tblForumThreads] ([ThreadID]) ON DELETE CASCADE NOT FOR REPLICATION
);


GO
CREATE CLUSTERED INDEX [IX_PostsRead]
    ON [dbo].[tblForumThreadsRead]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PostsRead_1]
    ON [dbo].[tblForumThreadsRead]([ThreadID] ASC);

