CREATE TABLE [dbo].[tblForumPostRating] (
    [UserID]   INT NOT NULL,
    [ThreadID] INT NOT NULL,
    [Rating]   INT NOT NULL,
    CONSTRAINT [FK_forums_PostRating_forums_Threads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[tblForumThreads] ([ThreadID]) ON DELETE CASCADE,
    CONSTRAINT [FK_forums_PostRating_forums_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblForumUsers] ([UserID]) ON DELETE CASCADE,
    CONSTRAINT [IX_forums_PostRating] UNIQUE NONCLUSTERED ([UserID] ASC, [ThreadID] ASC)
);

