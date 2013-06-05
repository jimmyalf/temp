CREATE TABLE [dbo].[tblForumForums] (
    [ForumID]                 INT             IDENTITY (1, 1) NOT NULL,
    [SiteID]                  INT             CONSTRAINT [DF_forums_Forums_SiteID] DEFAULT (0) NOT NULL,
    [IsActive]                SMALLINT        CONSTRAINT [DF_Forums_Active] DEFAULT (1) NOT NULL,
    [ParentID]                INT             CONSTRAINT [DF__Forums__ParentID__01342732] DEFAULT (0) NOT NULL,
    [ForumGroupID]            INT             NOT NULL,
    [Name]                    NVARCHAR (256)  NOT NULL,
    [NewsgroupName]           NVARCHAR (256)  CONSTRAINT [DF_forums_Forums_NewsgroupName] DEFAULT ('') NOT NULL,
    [Description]             NVARCHAR (1000) NOT NULL,
    [DateCreated]             DATETIME        CONSTRAINT [DF_Forums_DateCreated] DEFAULT (getdate()) NOT NULL,
    [Url]                     NVARCHAR (512)  CONSTRAINT [DF_forums_Forums_Url] DEFAULT ('') NOT NULL,
    [IsModerated]             SMALLINT        CONSTRAINT [DF_Forums_Moderated] DEFAULT (0) NOT NULL,
    [DaysToView]              INT             CONSTRAINT [DF_Forums_DaysToView] DEFAULT (7) NOT NULL,
    [SortOrder]               INT             CONSTRAINT [DF_Forums_SortOrder] DEFAULT (0) NOT NULL,
    [TotalPosts]              INT             CONSTRAINT [DF_Forums_TotalPosts] DEFAULT (0) NOT NULL,
    [TotalThreads]            INT             CONSTRAINT [DF_Forums_TotalThreads] DEFAULT (0) NOT NULL,
    [DisplayMask]             BINARY (512)    CONSTRAINT [DF__forums__DisplayM__004002F9] DEFAULT (0) NOT NULL,
    [EnablePostStatistics]    SMALLINT        CONSTRAINT [DF_forums_Forums_EnablePostStatistics] DEFAULT (1) NOT NULL,
    [EnableAutoDelete]        SMALLINT        CONSTRAINT [DF_forums_Forums_EnableAutoDelete] DEFAULT (0) NOT NULL,
    [EnableAnonymousPosting]  SMALLINT        CONSTRAINT [DF_forums_Forums_EnableAnonymousPosting] DEFAULT (0) NOT NULL,
    [AutoDeleteThreshold]     INT             CONSTRAINT [DF_forums_Forums_AutoDeleteThreshold] DEFAULT (90) NOT NULL,
    [MostRecentPostID]        INT             CONSTRAINT [DF_Forums_MostRecentPostID] DEFAULT (0) NOT NULL,
    [MostRecentThreadID]      INT             CONSTRAINT [DF_forums_Forums_MostRecentThreadID] DEFAULT (0) NOT NULL,
    [MostRecentThreadReplies] INT             CONSTRAINT [DF_forums_Forums_MostRecentThreadReplies] DEFAULT (0) NOT NULL,
    [MostRecentPostSubject]   NVARCHAR (64)   CONSTRAINT [DF_forums_Forums_MostRecentPostSubject] DEFAULT ('') NOT NULL,
    [MostRecentPostAuthor]    NVARCHAR (64)   CONSTRAINT [DF_forums_Forums_MostRecentPostAuthor] DEFAULT ('') NOT NULL,
    [MostRecentPostAuthorID]  INT             CONSTRAINT [DF_forums_Forums_MostRecentPostAuthorID] DEFAULT (0) NOT NULL,
    [MostRecentPostDate]      DATETIME        CONSTRAINT [DF_forums_Forums_MostRecentPostDate] DEFAULT ('1/1/1797') NOT NULL,
    [PostsToModerate]         INT             CONSTRAINT [DF_forums_Forums_PostsToModerate] DEFAULT (0) NOT NULL,
    [ForumType]               INT             CONSTRAINT [DF_forums_Forums_ForumType] DEFAULT (0) NOT NULL,
    [IsSearchable]            SMALLINT        CONSTRAINT [DF_forums_Forums_IsSearchable] DEFAULT (1) NOT NULL,
    CONSTRAINT [PK_Forums] PRIMARY KEY CLUSTERED ([ForumID] ASC, [SiteID] ASC, [IsActive] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Forums_Active]
    ON [dbo].[tblForumForums]([SiteID] ASC, [IsActive] ASC);


GO
CREATE NONCLUSTERED INDEX [ForumID_Active]
    ON [dbo].[tblForumForums]([SiteID] ASC, [ForumID] ASC);


GO


CREATE TRIGGER forums_Forum_Delete ON tblForumForums 
FOR DELETE 
AS
BEGIN
	DELETE tblForumForumPermissions WHERE ForumID IN (SELECT ForumID FROM DELETED)
	DELETE tblForumThreads WHERE ForumID IN (SELECT ForumID FROM DELETED)
END


