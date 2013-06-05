CREATE TABLE [dbo].[tblForumPosts] (
    [PostID]        INT            IDENTITY (1, 1) NOT NULL,
    [ThreadID]      INT            NOT NULL,
    [ParentID]      INT            NOT NULL,
    [PostAuthor]    NVARCHAR (64)  CONSTRAINT [DF_forums_Posts_Username] DEFAULT ('') NOT NULL,
    [UserID]        INT            NOT NULL,
    [ForumID]       INT            CONSTRAINT [DF_Posts_ForumID] DEFAULT (1) NOT NULL,
    [PostLevel]     INT            NOT NULL,
    [SortOrder]     INT            NOT NULL,
    [Subject]       NVARCHAR (256) NOT NULL,
    [PostDate]      DATETIME       CONSTRAINT [DF_Posts_PostDate] DEFAULT (getdate()) NOT NULL,
    [IsApproved]    BIT            CONSTRAINT [DF_Posts_Approved] DEFAULT (1) NOT NULL,
    [IsLocked]      BIT            CONSTRAINT [DF_forums_Posts_IsLocked] DEFAULT (0) NOT NULL,
    [IsIndexed]     BIT            CONSTRAINT [DF_forums_Posts_IsIndexed] DEFAULT (0) NOT NULL,
    [TotalViews]    INT            CONSTRAINT [DF_Posts_Views] DEFAULT (0) NOT NULL,
    [Body]          NTEXT          CONSTRAINT [DF__Posts__Body2__0B27A5C0] DEFAULT ('') NOT NULL,
    [FormattedBody] NTEXT          NOT NULL,
    [IPAddress]     NVARCHAR (32)  CONSTRAINT [DF_forums_Posts_IPAddress] DEFAULT (N'000.000.000.000') NOT NULL,
    [PostType]      INT            CONSTRAINT [DF__posts__PostType__290D0E62] DEFAULT (0) NOT NULL,
    [EmoticonID]    INT            CONSTRAINT [DF_forums_Posts_EmoticonID] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([PostID] ASC),
    CONSTRAINT [FK_forums_Posts_forums_Threads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[tblForumThreads] ([ThreadID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_ParentID]
    ON [dbo].[tblForumPosts]([ParentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_ThreadID]
    ON [dbo].[tblForumPosts]([ThreadID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_SortOrder]
    ON [dbo].[tblForumPosts]([SortOrder] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_PostLevel]
    ON [dbo].[tblForumPosts]([PostLevel] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_Approved]
    ON [dbo].[tblForumPosts]([IsApproved] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_ForumID]
    ON [dbo].[tblForumPosts]([ForumID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_PostDate]
    ON [dbo].[tblForumPosts]([UserID] ASC, [PostDate] ASC);


GO
CREATE NONCLUSTERED INDEX [ForumID_Approved]
    ON [dbo].[tblForumPosts]([ForumID] ASC, [IsApproved] ASC);

