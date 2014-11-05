CREATE TABLE [dbo].[tblForumForumPingback] (
    [ForumID]     INT            NOT NULL,
    [Pingback]    NVARCHAR (512) NOT NULL,
    [Count]       INT            CONSTRAINT [DF_forums_ForumPingback_Count] DEFAULT (0) NOT NULL,
    [LastUpdated] DATETIME       CONSTRAINT [DF_forums_ForumPingback_LastUpdated] DEFAULT (getdate()) NOT NULL
);


GO
CREATE CLUSTERED INDEX [IX_forums_ForumPingback]
    ON [dbo].[tblForumForumPingback]([ForumID] ASC);

