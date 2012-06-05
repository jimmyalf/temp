CREATE TABLE [dbo].[tblForumForumsRead] (
    [ForumGroupID]  INT      CONSTRAINT [DF_forums_ForumsRead_ForumGroupID] DEFAULT (0) NOT NULL,
    [ForumID]       INT      NOT NULL,
    [UserID]        INT      NOT NULL,
    [MarkReadAfter] INT      CONSTRAINT [DF_ForumsReadByDate_MarkReadAfter] DEFAULT (0) NOT NULL,
    [NewPosts]      BIT      CONSTRAINT [DF_forums_ForumsRead_NewPosts] DEFAULT (1) NOT NULL,
    [LastActivity]  DATETIME CONSTRAINT [DF_ForumsRead_LastActivity] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_forums_ForumsRead] PRIMARY KEY CLUSTERED ([UserID] ASC, [ForumID] ASC),
    CONSTRAINT [IX_ForumsReadByDate] UNIQUE NONCLUSTERED ([ForumID] ASC, [UserID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ForumsRead]
    ON [dbo].[tblForumForumsRead]([ForumID] ASC);

