CREATE TABLE [dbo].[tblForumTrackedForums] (
    [ForumID]          INT      NOT NULL,
    [UserID]           INT      NOT NULL,
    [SubscriptionType] INT      CONSTRAINT [DF_TrackedForums_SubscriptionType] DEFAULT (0) NOT NULL,
    [DateCreated]      DATETIME CONSTRAINT [DF_TrackedForums_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [IX_forums_TrackedForums] UNIQUE CLUSTERED ([ForumID] ASC, [UserID] ASC)
);

