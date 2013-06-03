CREATE TABLE [dbo].[tblForumThreads] (
    [ThreadID]               INT           IDENTITY (1, 1) NOT NULL,
    [ForumID]                INT           NOT NULL,
    [UserID]                 INT           NOT NULL,
    [PostAuthor]             NVARCHAR (64) CONSTRAINT [DF_forums_Threads_PostAuthor] DEFAULT ('') NOT NULL,
    [PostDate]               DATETIME      NOT NULL,
    [ThreadDate]             DATETIME      NOT NULL,
    [LastViewedDate]         DATETIME      CONSTRAINT [DF_forums_Threads_LastViewedDate] DEFAULT (getdate()) NOT NULL,
    [StickyDate]             DATETIME      NOT NULL,
    [TotalViews]             INT           CONSTRAINT [DF__Threads__TotalVi__5887175A] DEFAULT (0) NOT NULL,
    [TotalReplies]           INT           CONSTRAINT [DF__Threads__TotalRe__597B3B93] DEFAULT (0) NOT NULL,
    [MostRecentPostAuthorID] INT           NOT NULL,
    [MostRecentPostAuthor]   NVARCHAR (64) CONSTRAINT [DF_forums_Threads_MostRecentPostAuthor] DEFAULT ('') NOT NULL,
    [MostRecentPostID]       INT           NOT NULL,
    [IsLocked]               BIT           NOT NULL,
    [IsSticky]               BIT           NOT NULL,
    [IsApproved]             BIT           CONSTRAINT [DF_forums_Threads_IsApproved] DEFAULT (1) NOT NULL,
    [RatingSum]              INT           CONSTRAINT [DF_forums_Threads_RatingSum] DEFAULT (0) NOT NULL,
    [TotalRatings]           INT           CONSTRAINT [DF_forums_Threads_TotalRating] DEFAULT (0) NOT NULL,
    [ThreadEmoticonID]       INT           CONSTRAINT [DF_forums_Threads_ThreadEmoticon] DEFAULT (0) NOT NULL,
    [ThreadStatus]           INT           CONSTRAINT [DF_forums_Threads_ThreadStatus] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_forums_Threads] PRIMARY KEY CLUSTERED ([ThreadID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_Threads]
    ON [dbo].[tblForumThreads]([ForumID] ASC, [ThreadID] DESC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_Threads_StickyDate]
    ON [dbo].[tblForumThreads]([ForumID] ASC, [StickyDate] ASC, [IsApproved] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_Threads_1]
    ON [dbo].[tblForumThreads]([ForumID] ASC, [StickyDate] DESC);

