if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_SCHEDULE_TYPE_CD]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumServiceSchedule] DROP CONSTRAINT FK_SCHEDULE_TYPE_CD
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_SERVICE_TYPE_CODE]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumServices] DROP CONSTRAINT FK_SERVICE_TYPE_CODE
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_UserAvatar_forums_Images]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumUserAvatar] DROP CONSTRAINT FK_forums_UserAvatar_forums_Images
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_ModerationAudit_forums_ModerationAction]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumModerationAudit] DROP CONSTRAINT FK_forums_ModerationAudit_forums_ModerationAction
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_nntp_Posts_forums_Posts]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[nntp_Posts] DROP CONSTRAINT FK_nntp_Posts_forums_Posts
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_PostEditNotes_forums_Posts]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumPostEditNotes] DROP CONSTRAINT FK_forums_PostEditNotes_forums_Posts
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_SearchBarrel_forums_Posts]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumSearchBarrel] DROP CONSTRAINT FK_forums_SearchBarrel_forums_Posts
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_SERVICE_ID]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumServiceSchedule] DROP CONSTRAINT FK_SERVICE_ID
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_PostRating_forums_Threads]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumPostRating] DROP CONSTRAINT FK_forums_PostRating_forums_Threads
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_Posts_forums_Threads]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumPosts] DROP CONSTRAINT FK_forums_Posts_forums_Threads
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_SearchBarrel_forums_Threads]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumSearchBarrel] DROP CONSTRAINT FK_forums_SearchBarrel_forums_Threads
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_ThreadsRead_forums_Threads]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumThreadsRead] DROP CONSTRAINT FK_forums_ThreadsRead_forums_Threads
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_TrackedThreads_forums_Threads]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumTrackedThreads] DROP CONSTRAINT FK_forums_TrackedThreads_forums_Threads
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_PostRating_forums_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumPostRating] DROP CONSTRAINT FK_forums_PostRating_forums_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_UserAvatar_forums_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumUserAvatar] DROP CONSTRAINT FK_forums_UserAvatar_forums_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_forums_UserProfile_forums_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tblForumUserProfile] DROP CONSTRAINT FK_forums_UserProfile_forums_Users
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[nntp_Post_Delete]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[nntp_Post_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[forums_ForumGroup_Delete]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[forums_ForumGroup_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[forums_Forum_Delete]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[forums_Forum_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Vote]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Vote]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[nntp_Newsgroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[nntp_Newsgroups]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[nntp_Posts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[nntp_Posts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumAnonymousUsers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumAnonymousUsers]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumBlockedIpAddresses]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumBlockedIpAddresses]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumCensorship]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumCensorship]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumCodeScheduleType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumCodeScheduleType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumCodeServiceType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumCodeServiceType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumDisallowedNames]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumDisallowedNames]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumEmailQueue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumEmailQueue]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumExceptions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumExceptions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumForumGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumForumGroups]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumForumPermissions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumForumPermissions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumForumPingback]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumForumPingback]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumForums]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumForums]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumForumsRead]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumForumsRead]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumImages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumImages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumMessages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumMessages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumModerationAction]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumModerationAction]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumModerationAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumModerationAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumModerators]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumModerators]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPostAttachments]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPostAttachments]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPostEditNotes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPostEditNotes]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPostRating]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPostRating]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPosts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPosts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPostsArchive]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPostsArchive]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumPrivateMessages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumPrivateMessages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumRanks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumRanks]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumReports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumReports]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumRoles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumSearchBarrel]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumSearchBarrel]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumSearchIgnoreWords]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumSearchIgnoreWords]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumServiceSchedule]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumServiceSchedule]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumServices]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumServices]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumSiteSettings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumSiteSettings]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumSmilies]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumSmilies]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumThreads]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumThreads]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumThreadsRead]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumThreadsRead]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumTrackedForums]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumTrackedForums]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumTrackedThreads]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumTrackedThreads]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumUserAvatar]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumUserAvatar]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumUserProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumUserProfile]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumUsers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumUsers]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumUsersInRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumUsersInRoles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumUsersOnline]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumUsersOnline]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumVersion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumVersion]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumsStyles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumsStyles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumstatistics_Site]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumstatistics_Site]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tblForumstatistics_User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tblForumstatistics_User]
GO

CREATE TABLE [dbo].[Vote] (
	[PostID] [int] NOT NULL ,
	[Vote] [nvarchar] (2) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[VoteCount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[nntp_Newsgroups] (
	[ForumID] [int] NOT NULL ,
	[NntpGroup] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[NntpServer] [nvarchar] (100) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[nntp_Posts] (
	[PostID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[NntpPostID] [int] NOT NULL ,
	[NntpUniqueID] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumAnonymousUsers] (
	[UserID] [char] (36) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[LastLogin] [datetime] NOT NULL ,
	[LastAction] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumBlockedIpAddresses] (
	[IpID] [int] IDENTITY (1, 1) NOT NULL ,
	[Address] [nvarchar] (50) COLLATE Finnish_Swedish_CI_AS NULL ,
	[Reason] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumCensorship] (
	[Word] [nvarchar] (20) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Replacement] [nvarchar] (20) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumCodeScheduleType] (
	[ScheduleTypeCode] [int] IDENTITY (1, 1) NOT NULL ,
	[ScheduleDescription] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumCodeServiceType] (
	[ServiceTypeCode] [int] IDENTITY (1, 1) NOT NULL ,
	[ServiceTypeDescription] [nvarchar] (128) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumDisallowedNames] (
	[DisallowedName] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumEmailQueue] (
	[EmailID] [uniqueidentifier] NOT NULL ,
	[emailPriority] [int] NOT NULL ,
	[emailBodyFormat] [bit] NOT NULL ,
	[emailTo] [nvarchar] (2000) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[emailCc] [ntext] COLLATE Finnish_Swedish_CI_AS NULL ,
	[emailBcc] [nvarchar] (2000) COLLATE Finnish_Swedish_CI_AS NULL ,
	[EmailFrom] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[EmailSubject] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[EmailBody] [ntext] COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[CreatedTimestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumExceptions] (
	[ExceptionID] [int] IDENTITY (1, 1) NOT NULL ,
	[ExceptionHash] [varchar] (128) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[SiteID] [int] NOT NULL ,
	[Category] [int] NOT NULL ,
	[Exception] [nvarchar] (2000) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[ExceptionMessage] [nvarchar] (500) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[IPAddress] [varchar] (15) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[UserAgent] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[HttpReferrer] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[HttpVerb] [nvarchar] (24) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PathAndQuery] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[DateLastOccurred] [datetime] NOT NULL ,
	[Frequency] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumForumGroups] (
	[SiteID] [int] NOT NULL ,
	[ForumGroupID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[NewsgroupName] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[SortOrder] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumForumPermissions] (
	[ForumID] [int] NOT NULL ,
	[RoleID] [int] NOT NULL ,
	[View] [tinyint] NOT NULL ,
	[Read] [tinyint] NOT NULL ,
	[Post] [tinyint] NOT NULL ,
	[Reply] [tinyint] NOT NULL ,
	[Edit] [tinyint] NOT NULL ,
	[Delete] [tinyint] NOT NULL ,
	[Sticky] [tinyint] NOT NULL ,
	[Announce] [tinyint] NOT NULL ,
	[CreatePoll] [tinyint] NOT NULL ,
	[Vote] [tinyint] NOT NULL ,
	[Moderate] [tinyint] NOT NULL ,
	[Attachment] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumForumPingback] (
	[ForumID] [int] NOT NULL ,
	[Pingback] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Count] [int] NOT NULL ,
	[LastUpdated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumForums] (
	[ForumID] [int] IDENTITY (1, 1) NOT NULL ,
	[SiteID] [int] NOT NULL ,
	[IsActive] [smallint] NOT NULL ,
	[ParentID] [int] NOT NULL ,
	[ForumGroupID] [int] NOT NULL ,
	[Name] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[NewsgroupName] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Description] [nvarchar] (1000) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[Url] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[IsModerated] [smallint] NOT NULL ,
	[DaysToView] [int] NOT NULL ,
	[SortOrder] [int] NOT NULL ,
	[TotalPosts] [int] NOT NULL ,
	[TotalThreads] [int] NOT NULL ,
	[DisplayMask] [binary] (512) NOT NULL ,
	[EnablePostStatistics] [smallint] NOT NULL ,
	[EnableAutoDelete] [smallint] NOT NULL ,
	[EnableAnonymousPosting] [smallint] NOT NULL ,
	[AutoDeleteThreshold] [int] NOT NULL ,
	[MostRecentPostID] [int] NOT NULL ,
	[MostRecentThreadID] [int] NOT NULL ,
	[MostRecentThreadReplies] [int] NOT NULL ,
	[MostRecentPostSubject] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[MostRecentPostAuthor] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[MostRecentPostAuthorID] [int] NOT NULL ,
	[MostRecentPostDate] [datetime] NOT NULL ,
	[PostsToModerate] [int] NOT NULL ,
	[ForumType] [int] NOT NULL ,
	[IsSearchable] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumForumsRead] (
	[ForumGroupID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[UserID] [int] NOT NULL ,
	[MarkReadAfter] [int] NOT NULL ,
	[NewPosts] [bit] NOT NULL ,
	[LastActivity] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumImages] (
	[ImageID] [int] IDENTITY (1, 1) NOT NULL ,
	[Length] [int] NOT NULL ,
	[ContentType] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Content] [image] NOT NULL ,
	[DateLastUpdated] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumMessages] (
	[MessageID] [int] NOT NULL ,
	[Language] [nvarchar] (8) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Title] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Body] [ntext] COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumModerationAction] (
	[ModerationAction] [int] NOT NULL ,
	[Description] [nvarchar] (128) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[TotalActions] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumModerationAudit] (
	[ModerationAction] [int] NOT NULL ,
	[PostID] [int] NULL ,
	[UserID] [int] NULL ,
	[ForumID] [int] NULL ,
	[ModeratorID] [int] NOT NULL ,
	[ModeratedOn] [datetime] NOT NULL ,
	[Notes] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumModerators] (
	[UserID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[EmailNotification] [bit] NOT NULL ,
	[PostsModerated] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPostAttachments] (
	[PostID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[UserID] [int] NOT NULL ,
	[Created] [datetime] NOT NULL ,
	[FileName] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Content] [image] NOT NULL ,
	[ContentType] [nvarchar] (50) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[ContentSize] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPostEditNotes] (
	[PostID] [int] NOT NULL ,
	[EditNotes] [nvarchar] (4000) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPostRating] (
	[UserID] [int] NOT NULL ,
	[ThreadID] [int] NOT NULL ,
	[Rating] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPosts] (
	[PostID] [int] IDENTITY (1, 1) NOT NULL ,
	[ThreadID] [int] NOT NULL ,
	[ParentID] [int] NOT NULL ,
	[PostAuthor] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[UserID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[PostLevel] [int] NOT NULL ,
	[SortOrder] [int] NOT NULL ,
	[Subject] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PostDate] [datetime] NOT NULL ,
	[IsApproved] [bit] NOT NULL ,
	[IsLocked] [bit] NOT NULL ,
	[IsIndexed] [bit] NOT NULL ,
	[TotalViews] [int] NOT NULL ,
	[Body] [ntext] COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[FormattedBody] [ntext] COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[IPAddress] [nvarchar] (32) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PostType] [int] NOT NULL ,
	[EmoticonID] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPostsArchive] (
	[PostID] [int] IDENTITY (1, 1) NOT NULL ,
	[ThreadID] [int] NOT NULL ,
	[ParentID] [int] NOT NULL ,
	[PostLevel] [int] NOT NULL ,
	[SortOrder] [int] NOT NULL ,
	[Subject] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PostDate] [datetime] NOT NULL ,
	[Approved] [bit] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[UserName] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[ThreadDate] [datetime] NOT NULL ,
	[TotalViews] [int] NOT NULL ,
	[IsLocked] [bit] NOT NULL ,
	[IsPinned] [bit] NOT NULL ,
	[PinnedDate] [datetime] NOT NULL ,
	[Body] [ntext] COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumPrivateMessages] (
	[UserID] [int] NOT NULL ,
	[ThreadID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumRanks] (
	[RankID] [int] IDENTITY (1, 1) NOT NULL ,
	[RankName] [nvarchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[PostingCountMin] [int] NULL ,
	[PostingCountMax] [int] NULL ,
	[RankIconUrl] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumReports] (
	[ReportID] [int] IDENTITY (1, 1) NOT NULL ,
	[ReportName] [varchar] (20) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Active] [bit] NOT NULL ,
	[ReportCommand] [varchar] (6500) COLLATE Finnish_Swedish_CI_AS NULL ,
	[ReportScript] [text] COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumRoles] (
	[RoleID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Description] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumSearchBarrel] (
	[WordHash] [int] NOT NULL ,
	[Word] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PostID] [int] NOT NULL ,
	[ThreadID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[Weight] [float] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumSearchIgnoreWords] (
	[WordHash] [int] NOT NULL ,
	[Word] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumServiceSchedule] (
	[ServiceID] [int] NOT NULL ,
	[ScheduleName] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[MachineName] [varchar] (128) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[StartDate] [datetime] NOT NULL ,
	[EndDate] [datetime] NULL ,
	[ServiceParameters] [varchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[ScheduleTypeCode] [int] NOT NULL ,
	[RunTimeHour] [int] NOT NULL ,
	[RunTimeMinute] [int] NOT NULL ,
	[DelayHour] [int] NULL ,
	[DelayMinute] [int] NULL ,
	[RunDaily] [binary] (8) NULL ,
	[RunWeekly] [binary] (8) NULL ,
	[RunMonthly] [binary] (12) NULL ,
	[RunYearly] [tinyint] NULL ,
	[RunOnce] [tinyint] NULL ,
	[LastRunTime] [datetime] NULL ,
	[NextRunTime] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumServices] (
	[ServiceID] [int] IDENTITY (1, 1) NOT NULL ,
	[ServiceName] [nvarchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[ServiceTypeCode] [int] NULL ,
	[ServiceAssemblyPath] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[ServiceFullClassName] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[ServiceWorkingDirectory] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumSiteSettings] (
	[SiteID] [int] IDENTITY (1, 1) NOT NULL ,
	[Application] [nvarchar] (512) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[ForumsDisabled] [bit] NOT NULL ,
	[Settings] [image] NOT NULL ,
	[ForumsVersion] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumSmilies] (
	[SmileyID] [int] IDENTITY (1, 1) NOT NULL ,
	[SmileyCode] [nvarchar] (10) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[SmileyUrl] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[SmileyText] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[BracketSafe] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumThreads] (
	[ThreadID] [int] IDENTITY (1, 1) NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[UserID] [int] NOT NULL ,
	[PostAuthor] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PostDate] [datetime] NOT NULL ,
	[ThreadDate] [datetime] NOT NULL ,
	[LastViewedDate] [datetime] NOT NULL ,
	[StickyDate] [datetime] NOT NULL ,
	[TotalViews] [int] NOT NULL ,
	[TotalReplies] [int] NOT NULL ,
	[MostRecentPostAuthorID] [int] NOT NULL ,
	[MostRecentPostAuthor] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[MostRecentPostID] [int] NOT NULL ,
	[IsLocked] [bit] NOT NULL ,
	[IsSticky] [bit] NOT NULL ,
	[IsApproved] [bit] NOT NULL ,
	[RatingSum] [int] NOT NULL ,
	[TotalRatings] [int] NOT NULL ,
	[ThreadEmoticonID] [int] NOT NULL ,
	[ThreadStatus] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumThreadsRead] (
	[UserID] [int] NOT NULL ,
	[ForumGroupID] [int] NOT NULL ,
	[ForumID] [int] NOT NULL ,
	[ThreadID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumTrackedForums] (
	[ForumID] [int] NOT NULL ,
	[UserID] [int] NOT NULL ,
	[SubscriptionType] [int] NOT NULL ,
	[DateCreated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumTrackedThreads] (
	[ThreadID] [int] NOT NULL ,
	[UserID] [int] NULL ,
	[DateCreated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumUserAvatar] (
	[UserID] [int] NOT NULL ,
	[ImageID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumUserProfile] (
	[UserID] [int] NOT NULL ,
	[TimeZone] [float] NOT NULL ,
	[TotalPosts] [int] NOT NULL ,
	[PostSortOrder] [int] NOT NULL ,
	[StringNameValues] [varbinary] (7500) NOT NULL ,
	[PostRank] [binary] (1) NOT NULL ,
	[IsAvatarApproved] [smallint] NOT NULL ,
	[ModerationLevel] [smallint] NOT NULL ,
	[EnableThreadTracking] [smallint] NOT NULL ,
	[EnableDisplayUnreadThreadsOnly] [smallint] NOT NULL ,
	[EnableAvatar] [smallint] NOT NULL ,
	[EnableDisplayInMemberList] [smallint] NOT NULL ,
	[EnablePrivateMessages] [smallint] NOT NULL ,
	[EnableOnlineStatus] [smallint] NOT NULL ,
	[EnableHtmlEmail] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumUsers] (
	[UserID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserName] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[Password] [nvarchar] (64) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PasswordFormat] [smallint] NOT NULL ,
	[Salt] [varchar] (24) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[PasswordQuestion] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[PasswordAnswer] [nvarchar] (256) COLLATE Finnish_Swedish_CI_AS NULL ,
	[Email] [nvarchar] (128) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[DateCreated] [datetime] NOT NULL ,
	[LastLogin] [datetime] NOT NULL ,
	[LastActivity] [datetime] NOT NULL ,
	[LastAction] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[UserAccountStatus] [smallint] NOT NULL ,
	[IsAnonymous] [bit] NOT NULL ,
	[ForceLogin] [bit] NOT NULL ,
	[AppUserToken] [varchar] (128) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumUsersInRoles] (
	[UserID] [int] NOT NULL ,
	[RoleID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumUsersOnline] (
	[UserID] [int] NOT NULL ,
	[LastActivity] [datetime] NOT NULL ,
	[LastAction] [nvarchar] (1024) COLLATE Finnish_Swedish_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumVersion] (
	[VERSION_MAJOR] [int] NOT NULL ,
	[VERSION_MINOR] [int] NOT NULL ,
	[VERSION_REVISION] [int] NOT NULL ,
	[VERSION_BUILD] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumsStyles] (
	[StyleID] [int] IDENTITY (1, 1) NOT NULL ,
	[StyleName] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[StyleSheetTemplate] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[BodyBackgroundColor] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[BodyTextColor] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[LinkVisited] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[LinkHover] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[LinkActive] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[RowColorPrimary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[RowColorSecondary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[RowColorTertiary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[RowClassPrimary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[RowClassSecondary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[RowClassTertiary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[HeaderColorPrimary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[HeaderColorSecondary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[HeaderColorTertiary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[HeaderStylePrimary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[HeaderStyleSecondary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[HeaderStyleTertiary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[CellColorPrimary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[CellColorSecondary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[CellColorTertiary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[CellClassPrimary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[CellClassSecondary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[CellClassTertiary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[FontFacePrimary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[FontFaceSecondary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[FontFaceTertiary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[FontSizePrimary] [smallint] NULL ,
	[FontSizeSecondary] [smallint] NULL ,
	[FontSizeTertiary] [smallint] NULL ,
	[FontColorPrimary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[FontColorSecondary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[FontColorTertiary] [varchar] (6) COLLATE Finnish_Swedish_CI_AS NOT NULL ,
	[SpanClassPrimary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[SpanClassSecondary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL ,
	[SpanClassTertiary] [varchar] (30) COLLATE Finnish_Swedish_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumstatistics_Site] (
	[DateCreated] [datetime] NOT NULL ,
	[TotalUsers] [int] NOT NULL ,
	[TotalPosts] [int] NOT NULL ,
	[TotalModerators] [int] NOT NULL ,
	[TotalModeratedPosts] [int] NOT NULL ,
	[TotalAnonymousUsers] [int] NOT NULL ,
	[TotalTopics] [int] NOT NULL ,
	[DaysPosts] [int] NOT NULL ,
	[DaysTopics] [int] NOT NULL ,
	[NewPostsInPast24Hours] [int] NOT NULL ,
	[NewThreadsInPast24Hours] [int] NOT NULL ,
	[NewUsersInPast24Hours] [int] NOT NULL ,
	[MostViewsPostID] [int] NOT NULL ,
	[MostActivePostID] [int] NOT NULL ,
	[MostActiveUserID] [int] NOT NULL ,
	[MostReadPostID] [int] NOT NULL ,
	[NewestUserID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblForumstatistics_User] (
	[UserID] [int] NOT NULL ,
	[TotalPosts] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumBlockedIpAddresses] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_BlockedIpAddresses] PRIMARY KEY  CLUSTERED 
	(
		[IpID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumCensorship] WITH NOCHECK ADD 
	CONSTRAINT [PK_CENSORSHIP] PRIMARY KEY  CLUSTERED 
	(
		[Word]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumCodeScheduleType] WITH NOCHECK ADD 
	CONSTRAINT [PK_CODE_SCHEDULE_TYPE] PRIMARY KEY  CLUSTERED 
	(
		[ScheduleTypeCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumCodeServiceType] WITH NOCHECK ADD 
	CONSTRAINT [PK_SERVICE_TYPE_CODE] PRIMARY KEY  CLUSTERED 
	(
		[ServiceTypeCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumDisallowedNames] WITH NOCHECK ADD 
	CONSTRAINT [PK_DISALLOWED_NAME] PRIMARY KEY  CLUSTERED 
	(
		[DisallowedName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForumGroups] WITH NOCHECK ADD 
	CONSTRAINT [PK_ForumGroup] PRIMARY KEY  CLUSTERED 
	(
		[SiteID],
		[ForumGroupID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForumPermissions] WITH NOCHECK ADD 
	CONSTRAINT [IX_forums_ForumPermissions] UNIQUE  CLUSTERED 
	(
		[ForumID],
		[RoleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForums] WITH NOCHECK ADD 
	CONSTRAINT [PK_Forums] PRIMARY KEY  CLUSTERED 
	(
		[ForumID],
		[SiteID],
		[IsActive]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForumsRead] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_ForumsRead] PRIMARY KEY  CLUSTERED 
	(
		[UserID],
		[ForumID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumImages] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_Images] PRIMARY KEY  CLUSTERED 
	(
		[ImageID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumModerationAction] WITH NOCHECK ADD 
	CONSTRAINT [IX_forums_ModerationAction] UNIQUE  CLUSTERED 
	(
		[ModerationAction]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumModerators] WITH NOCHECK ADD 
	CONSTRAINT [PK_Moderators] PRIMARY KEY  CLUSTERED 
	(
		[UserID],
		[ForumID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumPostEditNotes] WITH NOCHECK ADD 
	CONSTRAINT [IX_forums_PostEditNotes] UNIQUE  CLUSTERED 
	(
		[PostID] DESC 
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumPosts] WITH NOCHECK ADD 
	CONSTRAINT [PK_Posts] PRIMARY KEY  CLUSTERED 
	(
		[PostID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumRanks] WITH NOCHECK ADD 
	CONSTRAINT [PK_RANK_ID] PRIMARY KEY  CLUSTERED 
	(
		[RankID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumReports] WITH NOCHECK ADD 
	CONSTRAINT [PK_REPORTS] PRIMARY KEY  CLUSTERED 
	(
		[ReportID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumRoles] WITH NOCHECK ADD 
	CONSTRAINT [IX_forums_Roles] UNIQUE  CLUSTERED 
	(
		[RoleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumServiceSchedule] WITH NOCHECK ADD 
	CONSTRAINT [PK_SERVICE_SCHEDULE] PRIMARY KEY  CLUSTERED 
	(
		[ServiceID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumServices] WITH NOCHECK ADD 
	CONSTRAINT [PK_SERVICE_ID] PRIMARY KEY  CLUSTERED 
	(
		[ServiceID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumSiteSettings] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_SiteSettings] PRIMARY KEY  CLUSTERED 
	(
		[SiteID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumSmilies] WITH NOCHECK ADD 
	CONSTRAINT [PK_SMILIES_ID] PRIMARY KEY  CLUSTERED 
	(
		[SmileyID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumThreads] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_Threads] PRIMARY KEY  CLUSTERED 
	(
		[ThreadID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumTrackedForums] WITH NOCHECK ADD 
	CONSTRAINT [IX_forums_TrackedForums] UNIQUE  CLUSTERED 
	(
		[ForumID],
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumUserProfile] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_UserProfile] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumUsers] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_Users] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumsStyles] WITH NOCHECK ADD 
	CONSTRAINT [PK_STYLE] PRIMARY KEY  CLUSTERED 
	(
		[StyleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumstatistics_Site] WITH NOCHECK ADD 
	CONSTRAINT [PK_forums_Statistics] PRIMARY KEY  CLUSTERED 
	(
		[DateCreated] DESC 
	)  ON [PRIMARY] 
GO

 CREATE  CLUSTERED  INDEX [IX_forums_ForumPingback] ON [dbo].[tblForumForumPingback]([ForumID]) ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_forums_PrivateMessages] ON [dbo].[tblForumPrivateMessages]([UserID]) ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_forums_SearchBarrel_1] ON [dbo].[tblForumSearchBarrel]([WordHash], [PostID], [Weight] DESC ) ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_PostsRead] ON [dbo].[tblForumThreadsRead]([UserID]) ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_forums_UserAvatar_1] ON [dbo].[tblForumUserAvatar]([UserID]) ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_forums_UserRoles] ON [dbo].[tblForumUsersInRoles]([UserID], [RoleID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[nntp_Posts] ADD 
	CONSTRAINT [DF_nntp_Posts_PostID] DEFAULT (0) FOR [PostID],
	CONSTRAINT [DF_nntp_Posts_ForumID] DEFAULT (0) FOR [ForumID]
GO

ALTER TABLE [dbo].[tblForumAnonymousUsers] ADD 
	CONSTRAINT [DF_AnonymousUsers_LastLogin] DEFAULT (getdate()) FOR [LastLogin],
	CONSTRAINT [DF_forums_AnonymousUsers_LastAction] DEFAULT ('') FOR [LastAction]
GO

 CREATE  INDEX [IX_AnonymousUsers] ON [dbo].[tblForumAnonymousUsers]([LastLogin]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumEmailQueue] ADD 
	CONSTRAINT [DF_forums_EmailQueue_EmailID] DEFAULT (newid()) FOR [EmailID],
	CONSTRAINT [DF_forums_EmailQueue_createdTimestamp] DEFAULT (getdate()) FOR [CreatedTimestamp]
GO

 CREATE  INDEX [IX_forums_EmailQueue] ON [dbo].[tblForumEmailQueue]([EmailID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumExceptions] ADD 
	CONSTRAINT [DF_forums_Exceptions_DateCreated] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_forums_Exceptions_Frequency] DEFAULT (0) FOR [Frequency],
	CONSTRAINT [IX_forums_Exceptions] UNIQUE  NONCLUSTERED 
	(
		[ExceptionID]
	)  ON [PRIMARY] ,
	CONSTRAINT [IX_forums_Exceptions_1] UNIQUE  NONCLUSTERED 
	(
		[ExceptionHash]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForumGroups] ADD 
	CONSTRAINT [DF_forums_ForumGroups_SiteID] DEFAULT (0) FOR [SiteID],
	CONSTRAINT [DF_forums_ForumGroups_NewsgroupFriendlyName] DEFAULT ('') FOR [NewsgroupName],
	CONSTRAINT [DF__ForumGrou__SortO__25518C17] DEFAULT (0) FOR [SortOrder],
	CONSTRAINT [IX_ForumGroups] UNIQUE  NONCLUSTERED 
	(
		[SiteID],
		[Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumForumPermissions] ADD 
	CONSTRAINT [DF_forums_ForumPermissions_View] DEFAULT (0) FOR [View],
	CONSTRAINT [DF_forums_ForumPermissions_Read] DEFAULT (0) FOR [Read],
	CONSTRAINT [DF_forums_ForumPermissions_Post] DEFAULT (0) FOR [Post],
	CONSTRAINT [DF_forums_ForumPermissions_Reply] DEFAULT (0) FOR [Reply],
	CONSTRAINT [DF_forums_ForumPermissions_Edit] DEFAULT (0) FOR [Edit],
	CONSTRAINT [DF_forums_ForumPermissions_Delete] DEFAULT (0) FOR [Delete],
	CONSTRAINT [DF_forums_ForumPermissions_Sticky] DEFAULT (0) FOR [Sticky],
	CONSTRAINT [DF_forums_ForumPermissions_Announce] DEFAULT (0) FOR [Announce],
	CONSTRAINT [DF_forums_ForumPermissions_CreatePoll] DEFAULT (0) FOR [CreatePoll],
	CONSTRAINT [DF_forums_ForumPermissions_Vote] DEFAULT (0) FOR [Vote],
	CONSTRAINT [DF_forums_ForumPermissions_Attachment] DEFAULT (0) FOR [Attachment]
GO

ALTER TABLE [dbo].[tblForumForumPingback] ADD 
	CONSTRAINT [DF_forums_ForumPingback_Count] DEFAULT (0) FOR [Count],
	CONSTRAINT [DF_forums_ForumPingback_LastUpdated] DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[tblForumForums] ADD 
	CONSTRAINT [DF_forums_Forums_SiteID] DEFAULT (0) FOR [SiteID],
	CONSTRAINT [DF_Forums_Active] DEFAULT (1) FOR [IsActive],
	CONSTRAINT [DF__Forums__ParentID__01342732] DEFAULT (0) FOR [ParentID],
	CONSTRAINT [DF_forums_Forums_NewsgroupName] DEFAULT ('') FOR [NewsgroupName],
	CONSTRAINT [DF_Forums_DateCreated] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_forums_Forums_Url] DEFAULT ('') FOR [Url],
	CONSTRAINT [DF_Forums_Moderated] DEFAULT (0) FOR [IsModerated],
	CONSTRAINT [DF_Forums_DaysToView] DEFAULT (7) FOR [DaysToView],
	CONSTRAINT [DF_Forums_SortOrder] DEFAULT (0) FOR [SortOrder],
	CONSTRAINT [DF_Forums_TotalPosts] DEFAULT (0) FOR [TotalPosts],
	CONSTRAINT [DF_Forums_TotalThreads] DEFAULT (0) FOR [TotalThreads],
	CONSTRAINT [DF__forums__DisplayM__004002F9] DEFAULT (0) FOR [DisplayMask],
	CONSTRAINT [DF_forums_Forums_EnablePostStatistics] DEFAULT (1) FOR [EnablePostStatistics],
	CONSTRAINT [DF_forums_Forums_EnableAutoDelete] DEFAULT (0) FOR [EnableAutoDelete],
	CONSTRAINT [DF_forums_Forums_EnableAnonymousPosting] DEFAULT (0) FOR [EnableAnonymousPosting],
	CONSTRAINT [DF_forums_Forums_AutoDeleteThreshold] DEFAULT (90) FOR [AutoDeleteThreshold],
	CONSTRAINT [DF_Forums_MostRecentPostID] DEFAULT (0) FOR [MostRecentPostID],
	CONSTRAINT [DF_forums_Forums_MostRecentThreadID] DEFAULT (0) FOR [MostRecentThreadID],
	CONSTRAINT [DF_forums_Forums_MostRecentThreadReplies] DEFAULT (0) FOR [MostRecentThreadReplies],
	CONSTRAINT [DF_forums_Forums_MostRecentPostSubject] DEFAULT ('') FOR [MostRecentPostSubject],
	CONSTRAINT [DF_forums_Forums_MostRecentPostAuthor] DEFAULT ('') FOR [MostRecentPostAuthor],
	CONSTRAINT [DF_forums_Forums_MostRecentPostAuthorID] DEFAULT (0) FOR [MostRecentPostAuthorID],
	CONSTRAINT [DF_forums_Forums_MostRecentPostDate] DEFAULT ('1/1/1797') FOR [MostRecentPostDate],
	CONSTRAINT [DF_forums_Forums_PostsToModerate] DEFAULT (0) FOR [PostsToModerate],
	CONSTRAINT [DF_forums_Forums_ForumType] DEFAULT (0) FOR [ForumType],
	CONSTRAINT [DF_forums_Forums_IsSearchable] DEFAULT (1) FOR [IsSearchable]
GO

 CREATE  INDEX [IX_Forums_Active] ON [dbo].[tblForumForums]([SiteID], [IsActive]) ON [PRIMARY]
GO

 CREATE  INDEX [ForumID_Active] ON [dbo].[tblForumForums]([SiteID], [ForumID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumForumsRead] ADD 
	CONSTRAINT [DF_forums_ForumsRead_ForumGroupID] DEFAULT (0) FOR [ForumGroupID],
	CONSTRAINT [DF_ForumsReadByDate_MarkReadAfter] DEFAULT (0) FOR [MarkReadAfter],
	CONSTRAINT [DF_forums_ForumsRead_NewPosts] DEFAULT (1) FOR [NewPosts],
	CONSTRAINT [DF_ForumsRead_LastActivity] DEFAULT (getdate()) FOR [LastActivity],
	CONSTRAINT [IX_ForumsReadByDate] UNIQUE  NONCLUSTERED 
	(
		[ForumID],
		[UserID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_ForumsRead] ON [dbo].[tblForumForumsRead]([ForumID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumImages] ADD 
	CONSTRAINT [DF_forums_Images_DateLastUpdated] DEFAULT (getdate()) FOR [DateLastUpdated]
GO

ALTER TABLE [dbo].[tblForumMessages] ADD 
	CONSTRAINT [DF_forums_Messages_Language] DEFAULT ('en-US') FOR [Language]
GO

ALTER TABLE [dbo].[tblForumModerationAction] ADD 
	CONSTRAINT [DF_ModerationAction_TotalActions] DEFAULT (0) FOR [TotalActions]
GO

ALTER TABLE [dbo].[tblForumModerationAudit] ADD 
	CONSTRAINT [DF_forums_ModerationAudit_ModeratedOn] DEFAULT (getdate()) FOR [ModeratedOn]
GO

 CREATE  INDEX [IX_forums_ModerationAudit] ON [dbo].[tblForumModerationAudit]([ModerationAction]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumModerators] ADD 
	CONSTRAINT [DF_Moderators_DateCreated] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_Moderators_EmailNotification] DEFAULT (0) FOR [EmailNotification],
	CONSTRAINT [DF_Moderators_PostsModerated] DEFAULT (0) FOR [PostsModerated]
GO

ALTER TABLE [dbo].[tblForumPostAttachments] ADD 
	CONSTRAINT [DF_forums_PostAttachments_UserID] DEFAULT (0) FOR [UserID],
	CONSTRAINT [DF_forums_PostAttachments_Created] DEFAULT (getdate()) FOR [Created]
GO

 CREATE  INDEX [IX_forums_PostAttachments] ON [dbo].[tblForumPostAttachments]([PostID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumPostEditNotes] ADD 
	CONSTRAINT [PK_forums_PostEditNotes] PRIMARY KEY  NONCLUSTERED 
	(
		[PostID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumPostRating] ADD 
	CONSTRAINT [IX_forums_PostRating] UNIQUE  NONCLUSTERED 
	(
		[UserID],
		[ThreadID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumPosts] ADD 
	CONSTRAINT [DF_forums_Posts_Username] DEFAULT ('') FOR [PostAuthor],
	CONSTRAINT [DF_Posts_ForumID] DEFAULT (1) FOR [ForumID],
	CONSTRAINT [DF_Posts_PostDate] DEFAULT (getdate()) FOR [PostDate],
	CONSTRAINT [DF_Posts_Approved] DEFAULT (1) FOR [IsApproved],
	CONSTRAINT [DF_forums_Posts_IsLocked] DEFAULT (0) FOR [IsLocked],
	CONSTRAINT [DF_forums_Posts_IsIndexed] DEFAULT (0) FOR [IsIndexed],
	CONSTRAINT [DF_Posts_Views] DEFAULT (0) FOR [TotalViews],
	CONSTRAINT [DF__Posts__Body2__0B27A5C0] DEFAULT ('') FOR [Body],
	CONSTRAINT [DF_forums_Posts_IPAddress] DEFAULT (N'000.000.000.000') FOR [IPAddress],
	CONSTRAINT [DF__posts__PostType__290D0E62] DEFAULT (0) FOR [PostType],
	CONSTRAINT [DF_forums_Posts_EmoticonID] DEFAULT (0) FOR [EmoticonID]
GO

 CREATE  INDEX [IX_Posts_ParentID] ON [dbo].[tblForumPosts]([ParentID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_ThreadID] ON [dbo].[tblForumPosts]([ThreadID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_SortOrder] ON [dbo].[tblForumPosts]([SortOrder]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_PostLevel] ON [dbo].[tblForumPosts]([PostLevel]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_Approved] ON [dbo].[tblForumPosts]([IsApproved]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_ForumID] ON [dbo].[tblForumPosts]([ForumID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Posts_PostDate] ON [dbo].[tblForumPosts]([UserID], [PostDate]) ON [PRIMARY]
GO

 CREATE  INDEX [ForumID_Approved] ON [dbo].[tblForumPosts]([ForumID], [IsApproved]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_forums_PrivateMessages_1] ON [dbo].[tblForumPrivateMessages]([ThreadID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumRanks] ADD 
	CONSTRAINT [UK_RANK_NAME] UNIQUE  NONCLUSTERED 
	(
		[RankName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumSearchBarrel] ADD 
	CONSTRAINT [DF_forums_SearchBarrel_word] DEFAULT ('') FOR [Word],
	CONSTRAINT [DF_forums_SearchBarrel_threadId_1] DEFAULT (0) FOR [ThreadID],
	CONSTRAINT [DF_forums_SearchBarrel_forumId] DEFAULT (0) FOR [ForumID],
	CONSTRAINT [DF_forums_SearchBarrel_weight] DEFAULT (0) FOR [Weight],
	CONSTRAINT [IX_forums_SearchBarrel] UNIQUE  NONCLUSTERED 
	(
		[WordHash],
		[PostID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_forums_SearchWordDictionary] ON [dbo].[tblForumSearchIgnoreWords]([WordHash]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumServices] ADD 
	CONSTRAINT [UK_SERVICE_NAME] UNIQUE  NONCLUSTERED 
	(
		[ServiceName]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tblForumSiteSettings] ADD 
	CONSTRAINT [DF_forums_SiteSettings_ForumsDirectory] DEFAULT (N'/AspNetForums') FOR [Application],
	CONSTRAINT [DF_forums_SiteSettings_Enabled] DEFAULT (0) FOR [ForumsDisabled],
	CONSTRAINT [DF_forums_SiteSettings_Settings] DEFAULT ('') FOR [Settings]
GO

ALTER TABLE [dbo].[tblForumSmilies] ADD 
	CONSTRAINT [DF__forums_Sm__Brack__173876EA] DEFAULT (0) FOR [BracketSafe]
GO

ALTER TABLE [dbo].[tblForumThreads] ADD 
	CONSTRAINT [DF_forums_Threads_PostAuthor] DEFAULT ('') FOR [PostAuthor],
	CONSTRAINT [DF_forums_Threads_LastViewedDate] DEFAULT (getdate()) FOR [LastViewedDate],
	CONSTRAINT [DF__Threads__TotalVi__5887175A] DEFAULT (0) FOR [TotalViews],
	CONSTRAINT [DF__Threads__TotalRe__597B3B93] DEFAULT (0) FOR [TotalReplies],
	CONSTRAINT [DF_forums_Threads_MostRecentPostAuthor] DEFAULT ('') FOR [MostRecentPostAuthor],
	CONSTRAINT [DF_forums_Threads_IsApproved] DEFAULT (1) FOR [IsApproved],
	CONSTRAINT [DF_forums_Threads_RatingSum] DEFAULT (0) FOR [RatingSum],
	CONSTRAINT [DF_forums_Threads_TotalRating] DEFAULT (0) FOR [TotalRatings],
	CONSTRAINT [DF_forums_Threads_ThreadEmoticon] DEFAULT (0) FOR [ThreadEmoticonID],
	CONSTRAINT [DF_forums_Threads_ThreadStatus] DEFAULT (0) FOR [ThreadStatus]
GO

 CREATE  INDEX [IX_forums_Threads] ON [dbo].[tblForumThreads]([ForumID], [ThreadID] DESC ) ON [PRIMARY]
GO

 CREATE  INDEX [IX_forums_Threads_StickyDate] ON [dbo].[tblForumThreads]([ForumID], [StickyDate], [IsApproved]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_forums_Threads_1] ON [dbo].[tblForumThreads]([ForumID], [StickyDate] DESC ) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumThreadsRead] ADD 
	CONSTRAINT [DF_forums_ThreadsRead_ForumGroupID] DEFAULT (0) FOR [ForumGroupID],
	CONSTRAINT [DF_forums_ThreadsRead_ForumID] DEFAULT (0) FOR [ForumID]
GO

 CREATE  INDEX [IX_PostsRead_1] ON [dbo].[tblForumThreadsRead]([ThreadID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumTrackedForums] ADD 
	CONSTRAINT [DF_TrackedForums_SubscriptionType] DEFAULT (0) FOR [SubscriptionType],
	CONSTRAINT [DF_TrackedForums_DateCreated] DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[tblForumTrackedThreads] ADD 
	CONSTRAINT [DF_ThreadTrackings_DateCreated] DEFAULT (getdate()) FOR [DateCreated]
GO

 CREATE  INDEX [IX_forums_UserAvatar] ON [dbo].[tblForumUserAvatar]([UserID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumUserProfile] ADD 
	CONSTRAINT [DF_forums_UserProfile_TimeZone] DEFAULT ((-5)) FOR [TimeZone],
	CONSTRAINT [DF_forums_UserProfile_TotalPosts] DEFAULT (0) FOR [TotalPosts],
	CONSTRAINT [DF_forums_UserProfile_PostSortOrder] DEFAULT (0) FOR [PostSortOrder],
	CONSTRAINT [DF_forums_UserProfile_StringNameValues_1] DEFAULT (0) FOR [StringNameValues],
	CONSTRAINT [DF_forums_UserProfile_Attributes] DEFAULT (0) FOR [PostRank],
	CONSTRAINT [DF_forums_UserProfile_IsAvatarApproved] DEFAULT (1) FOR [IsAvatarApproved],
	CONSTRAINT [DF_forums_UserProfile_IsTrusted] DEFAULT (0) FOR [ModerationLevel],
	CONSTRAINT [DF_forums_UserProfile_TrackYourPosts] DEFAULT (0) FOR [EnableThreadTracking],
	CONSTRAINT [DF_forums_UserProfile_ShowUnreadTopicsOnly] DEFAULT (0) FOR [EnableDisplayUnreadThreadsOnly],
	CONSTRAINT [DF_forums_UserProfile_EnableAvatar] DEFAULT (0) FOR [EnableAvatar],
	CONSTRAINT [DF_forums_UserProfile_EnableDisplayInMemberList] DEFAULT (1) FOR [EnableDisplayInMemberList],
	CONSTRAINT [DF_forums_UserProfile_EnablePrivateMessages] DEFAULT (1) FOR [EnablePrivateMessages],
	CONSTRAINT [DF_forums_UserProfile_EnableOnlineStatus] DEFAULT (1) FOR [EnableOnlineStatus],
	CONSTRAINT [DF_forums_UserProfile_EnableHtmlEmail] DEFAULT (1) FOR [EnableHtmlEmail]
GO

 CREATE  INDEX [IX_forums_UserProfile] ON [dbo].[tblForumUserProfile]([TotalPosts] DESC ) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumUsers] ADD 
	CONSTRAINT [DF_forums_Users_PasswordFormat] DEFAULT (0) FOR [PasswordFormat],
	CONSTRAINT [DF_forums_Users_Salt] DEFAULT ('') FOR [Salt],
	CONSTRAINT [DF_forums_Users_PasswordQuestion] DEFAULT ('') FOR [PasswordQuestion],
	CONSTRAINT [DF_Users_DateCreated] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_Users_LastLogin] DEFAULT (getdate()) FOR [LastLogin],
	CONSTRAINT [DF_Users_LastActivity] DEFAULT (getdate()) FOR [LastActivity],
	CONSTRAINT [DF_forums_Users_LastAction] DEFAULT ('') FOR [LastAction],
	CONSTRAINT [DF_Users_Approved] DEFAULT (1) FOR [UserAccountStatus],
	CONSTRAINT [DF_forums_Users_IsAnonymous] DEFAULT (0) FOR [IsAnonymous],
	CONSTRAINT [DF_forums_Users_ForcLogin] DEFAULT (0) FOR [ForceLogin],
	CONSTRAINT [IX_forums_Users_2] UNIQUE  NONCLUSTERED 
	(
		[UserName]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_Users] ON [dbo].[tblForumUsers]([DateCreated]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_forums_Users] ON [dbo].[tblForumUsers]([UserID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_forums_Users_1] ON [dbo].[tblForumUsers]([Email]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumUsersOnline] ADD 
	CONSTRAINT [DF_forums_UsersOnline_UserID] DEFAULT (0) FOR [UserID],
	CONSTRAINT [DF_forums_UsersOnline_LastAction] DEFAULT ('') FOR [LastAction],
	CONSTRAINT [IX_forums_UsersOnline] UNIQUE  NONCLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_forums_UsersOnline_1] ON [dbo].[tblForumUsersOnline]([LastActivity]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblForumstatistics_Site] ADD 
	CONSTRAINT [DF_forums_Statistics_DateCreated] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_forums_Statistics_TotalAnonymousUsers] DEFAULT (0) FOR [TotalAnonymousUsers],
	CONSTRAINT [DF_forums_statistics_Site_NewestUserID] DEFAULT (0) FOR [NewestUserID]
GO

ALTER TABLE [dbo].[tblForumstatistics_User] ADD 
	CONSTRAINT [DF_forums_statistics_User_UserID] DEFAULT (0) FOR [UserID],
	CONSTRAINT [DF_forums_MostActiveUsers_TotalPosts] DEFAULT (0) FOR [TotalPosts]
GO

 CREATE  INDEX [IX_forums_MostActiveUsers] ON [dbo].[tblForumstatistics_User]([TotalPosts] DESC ) ON [PRIMARY]
GO

ALTER TABLE [dbo].[nntp_Posts] ADD 
	CONSTRAINT [FK_nntp_Posts_forums_Posts] FOREIGN KEY 
	(
		[PostID]
	) REFERENCES [dbo].[tblForumPosts] (
		[PostID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[tblForumModerationAudit] ADD 
	CONSTRAINT [FK_forums_ModerationAudit_forums_ModerationAction] FOREIGN KEY 
	(
		[ModerationAction]
	) REFERENCES [dbo].[tblForumModerationAction] (
		[ModerationAction]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[tblForumPostEditNotes] ADD 
	CONSTRAINT [FK_forums_PostEditNotes_forums_Posts] FOREIGN KEY 
	(
		[PostID]
	) REFERENCES [dbo].[tblForumPosts] (
		[PostID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[tblForumPostRating] ADD 
	CONSTRAINT [FK_forums_PostRating_forums_Threads] FOREIGN KEY 
	(
		[ThreadID]
	) REFERENCES [dbo].[tblForumThreads] (
		[ThreadID]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_forums_PostRating_forums_Users] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [dbo].[tblForumUsers] (
		[UserID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[tblForumPosts] ADD 
	CONSTRAINT [FK_forums_Posts_forums_Threads] FOREIGN KEY 
	(
		[ThreadID]
	) REFERENCES [dbo].[tblForumThreads] (
		[ThreadID]
	)
GO

ALTER TABLE [dbo].[tblForumSearchBarrel] ADD 
	CONSTRAINT [FK_forums_SearchBarrel_forums_Posts] FOREIGN KEY 
	(
		[PostID]
	) REFERENCES [dbo].[tblForumPosts] (
		[PostID]
	) ON DELETE CASCADE  NOT FOR REPLICATION ,
	CONSTRAINT [FK_forums_SearchBarrel_forums_Threads] FOREIGN KEY 
	(
		[ThreadID]
	) REFERENCES [dbo].[tblForumThreads] (
		[ThreadID]
	) ON DELETE CASCADE  NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[tblForumServiceSchedule] ADD 
	CONSTRAINT [FK_SCHEDULE_TYPE_CD] FOREIGN KEY 
	(
		[ScheduleTypeCode]
	) REFERENCES [dbo].[tblForumCodeScheduleType] (
		[ScheduleTypeCode]
	),
	CONSTRAINT [FK_SERVICE_ID] FOREIGN KEY 
	(
		[ServiceID]
	) REFERENCES [dbo].[tblForumServices] (
		[ServiceID]
	)
GO

ALTER TABLE [dbo].[tblForumServices] ADD 
	CONSTRAINT [FK_SERVICE_TYPE_CODE] FOREIGN KEY 
	(
		[ServiceTypeCode]
	) REFERENCES [dbo].[tblForumCodeServiceType] (
		[ServiceTypeCode]
	)
GO

ALTER TABLE [dbo].[tblForumThreadsRead] ADD 
	CONSTRAINT [FK_forums_ThreadsRead_forums_Threads] FOREIGN KEY 
	(
		[ThreadID]
	) REFERENCES [dbo].[tblForumThreads] (
		[ThreadID]
	) ON DELETE CASCADE  NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[tblForumTrackedThreads] ADD 
	CONSTRAINT [FK_forums_TrackedThreads_forums_Threads] FOREIGN KEY 
	(
		[ThreadID]
	) REFERENCES [dbo].[tblForumThreads] (
		[ThreadID]
	) ON DELETE CASCADE  NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[tblForumUserAvatar] ADD 
	CONSTRAINT [FK_forums_UserAvatar_forums_Images] FOREIGN KEY 
	(
		[ImageID]
	) REFERENCES [dbo].[tblForumImages] (
		[ImageID]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_forums_UserAvatar_forums_Users] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [dbo].[tblForumUsers] (
		[UserID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[tblForumUserProfile] ADD 
	CONSTRAINT [FK_forums_UserProfile_forums_Users] FOREIGN KEY 
	(
		[UserID]
	) REFERENCES [dbo].[tblForumUsers] (
		[UserID]
	) ON DELETE CASCADE 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER nntp_Post_Delete ON [nntp_Posts] 
FOR DELETE 
AS
BEGIN
	DELETE forums_Posts WHERE PostID IN (SELECT PostID FROM DELETED)
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER forums_ForumGroup_Delete ON tblForumForumGroups 
FOR DELETE 
AS
BEGIN
	DELETE tblForumForums WHERE ForumGroupID IN (SELECT ForumGroupID FROM DELETED)
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER forums_Forum_Delete ON tblForumForums 
FOR DELETE 
AS
BEGIN
	DELETE tblForumForumPermissions WHERE ForumID IN (SELECT ForumID FROM DELETED)
	DELETE tblForumThreads WHERE ForumID IN (SELECT ForumID FROM DELETED)
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

