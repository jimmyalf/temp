CREATE TABLE [dbo].[tblForumstatistics_Site] (
    [DateCreated]             DATETIME CONSTRAINT [DF_forums_Statistics_DateCreated] DEFAULT (getdate()) NOT NULL,
    [TotalUsers]              INT      NOT NULL,
    [TotalPosts]              INT      NOT NULL,
    [TotalModerators]         INT      NOT NULL,
    [TotalModeratedPosts]     INT      NOT NULL,
    [TotalAnonymousUsers]     INT      CONSTRAINT [DF_forums_Statistics_TotalAnonymousUsers] DEFAULT (0) NOT NULL,
    [TotalTopics]             INT      NOT NULL,
    [DaysPosts]               INT      NOT NULL,
    [DaysTopics]              INT      NOT NULL,
    [NewPostsInPast24Hours]   INT      NOT NULL,
    [NewThreadsInPast24Hours] INT      NOT NULL,
    [NewUsersInPast24Hours]   INT      NOT NULL,
    [MostViewsPostID]         INT      NOT NULL,
    [MostActivePostID]        INT      NOT NULL,
    [MostActiveUserID]        INT      NOT NULL,
    [MostReadPostID]          INT      NOT NULL,
    [NewestUserID]            INT      CONSTRAINT [DF_forums_statistics_Site_NewestUserID] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_forums_Statistics] PRIMARY KEY CLUSTERED ([DateCreated] DESC)
);

