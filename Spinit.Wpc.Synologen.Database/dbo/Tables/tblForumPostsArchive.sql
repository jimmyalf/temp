CREATE TABLE [dbo].[tblForumPostsArchive] (
    [PostID]     INT            IDENTITY (1, 1) NOT NULL,
    [ThreadID]   INT            NOT NULL,
    [ParentID]   INT            NOT NULL,
    [PostLevel]  INT            NOT NULL,
    [SortOrder]  INT            NOT NULL,
    [Subject]    NVARCHAR (256) NOT NULL,
    [PostDate]   DATETIME       NOT NULL,
    [Approved]   BIT            NOT NULL,
    [ForumID]    INT            NOT NULL,
    [UserName]   NVARCHAR (64)  NOT NULL,
    [ThreadDate] DATETIME       NOT NULL,
    [TotalViews] INT            NOT NULL,
    [IsLocked]   BIT            NOT NULL,
    [IsPinned]   BIT            NOT NULL,
    [PinnedDate] DATETIME       NOT NULL,
    [Body]       NTEXT          NOT NULL
);

