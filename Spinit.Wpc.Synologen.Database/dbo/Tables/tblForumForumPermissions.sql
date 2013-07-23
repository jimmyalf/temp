CREATE TABLE [dbo].[tblForumForumPermissions] (
    [ForumID]    INT     NOT NULL,
    [RoleID]     INT     NOT NULL,
    [View]       TINYINT CONSTRAINT [DF_forums_ForumPermissions_View] DEFAULT (0) NOT NULL,
    [Read]       TINYINT CONSTRAINT [DF_forums_ForumPermissions_Read] DEFAULT (0) NOT NULL,
    [Post]       TINYINT CONSTRAINT [DF_forums_ForumPermissions_Post] DEFAULT (0) NOT NULL,
    [Reply]      TINYINT CONSTRAINT [DF_forums_ForumPermissions_Reply] DEFAULT (0) NOT NULL,
    [Edit]       TINYINT CONSTRAINT [DF_forums_ForumPermissions_Edit] DEFAULT (0) NOT NULL,
    [Delete]     TINYINT CONSTRAINT [DF_forums_ForumPermissions_Delete] DEFAULT (0) NOT NULL,
    [Sticky]     TINYINT CONSTRAINT [DF_forums_ForumPermissions_Sticky] DEFAULT (0) NOT NULL,
    [Announce]   TINYINT CONSTRAINT [DF_forums_ForumPermissions_Announce] DEFAULT (0) NOT NULL,
    [CreatePoll] TINYINT CONSTRAINT [DF_forums_ForumPermissions_CreatePoll] DEFAULT (0) NOT NULL,
    [Vote]       TINYINT CONSTRAINT [DF_forums_ForumPermissions_Vote] DEFAULT (0) NOT NULL,
    [Moderate]   TINYINT NOT NULL,
    [Attachment] TINYINT CONSTRAINT [DF_forums_ForumPermissions_Attachment] DEFAULT (0) NOT NULL,
    CONSTRAINT [IX_forums_ForumPermissions] UNIQUE CLUSTERED ([ForumID] ASC, [RoleID] ASC)
);

