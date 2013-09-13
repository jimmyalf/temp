CREATE TABLE [dbo].[tblForumUserProfile] (
    [UserID]                         INT              NOT NULL,
    [TimeZone]                       FLOAT (53)       CONSTRAINT [DF_forums_UserProfile_TimeZone] DEFAULT ((-5)) NOT NULL,
    [TotalPosts]                     INT              CONSTRAINT [DF_forums_UserProfile_TotalPosts] DEFAULT (0) NOT NULL,
    [PostSortOrder]                  INT              CONSTRAINT [DF_forums_UserProfile_PostSortOrder] DEFAULT (0) NOT NULL,
    [StringNameValues]               VARBINARY (7500) CONSTRAINT [DF_forums_UserProfile_StringNameValues_1] DEFAULT (0) NOT NULL,
    [PostRank]                       BINARY (1)       CONSTRAINT [DF_forums_UserProfile_Attributes] DEFAULT (0) NOT NULL,
    [IsAvatarApproved]               SMALLINT         CONSTRAINT [DF_forums_UserProfile_IsAvatarApproved] DEFAULT (1) NOT NULL,
    [ModerationLevel]                SMALLINT         CONSTRAINT [DF_forums_UserProfile_IsTrusted] DEFAULT (0) NOT NULL,
    [EnableThreadTracking]           SMALLINT         CONSTRAINT [DF_forums_UserProfile_TrackYourPosts] DEFAULT (0) NOT NULL,
    [EnableDisplayUnreadThreadsOnly] SMALLINT         CONSTRAINT [DF_forums_UserProfile_ShowUnreadTopicsOnly] DEFAULT (0) NOT NULL,
    [EnableAvatar]                   SMALLINT         CONSTRAINT [DF_forums_UserProfile_EnableAvatar] DEFAULT (0) NOT NULL,
    [EnableDisplayInMemberList]      SMALLINT         CONSTRAINT [DF_forums_UserProfile_EnableDisplayInMemberList] DEFAULT (1) NOT NULL,
    [EnablePrivateMessages]          SMALLINT         CONSTRAINT [DF_forums_UserProfile_EnablePrivateMessages] DEFAULT (1) NOT NULL,
    [EnableOnlineStatus]             SMALLINT         CONSTRAINT [DF_forums_UserProfile_EnableOnlineStatus] DEFAULT (1) NOT NULL,
    [EnableHtmlEmail]                SMALLINT         CONSTRAINT [DF_forums_UserProfile_EnableHtmlEmail] DEFAULT (1) NOT NULL,
    CONSTRAINT [PK_forums_UserProfile] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_forums_UserProfile_forums_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblForumUsers] ([UserID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_UserProfile]
    ON [dbo].[tblForumUserProfile]([TotalPosts] DESC);

