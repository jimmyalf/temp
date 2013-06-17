CREATE TABLE [dbo].[tblForumModerators] (
    [UserID]            INT      NOT NULL,
    [ForumID]           INT      NOT NULL,
    [DateCreated]       DATETIME CONSTRAINT [DF_Moderators_DateCreated] DEFAULT (getdate()) NOT NULL,
    [EmailNotification] BIT      CONSTRAINT [DF_Moderators_EmailNotification] DEFAULT (0) NOT NULL,
    [PostsModerated]    INT      CONSTRAINT [DF_Moderators_PostsModerated] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_Moderators] PRIMARY KEY CLUSTERED ([UserID] ASC, [ForumID] ASC)
);

