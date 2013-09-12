CREATE TABLE [dbo].[tblForumTrackedThreads] (
    [ThreadID]    INT      NOT NULL,
    [UserID]      INT      NULL,
    [DateCreated] DATETIME CONSTRAINT [DF_ThreadTrackings_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [FK_forums_TrackedThreads_forums_Threads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[tblForumThreads] ([ThreadID]) ON DELETE CASCADE NOT FOR REPLICATION
);

