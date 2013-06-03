CREATE TABLE [dbo].[tblForumForumGroups] (
    [SiteID]        INT            CONSTRAINT [DF_forums_ForumGroups_SiteID] DEFAULT (0) NOT NULL,
    [ForumGroupID]  INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (256) NOT NULL,
    [NewsgroupName] NVARCHAR (256) CONSTRAINT [DF_forums_ForumGroups_NewsgroupFriendlyName] DEFAULT ('') NOT NULL,
    [SortOrder]     INT            CONSTRAINT [DF__ForumGrou__SortO__25518C17] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_ForumGroup] PRIMARY KEY CLUSTERED ([SiteID] ASC, [ForumGroupID] ASC),
    CONSTRAINT [IX_ForumGroups] UNIQUE NONCLUSTERED ([SiteID] ASC, [Name] ASC)
);


GO


CREATE TRIGGER forums_ForumGroup_Delete ON tblForumForumGroups 
FOR DELETE 
AS
BEGIN
	DELETE tblForumForums WHERE ForumGroupID IN (SELECT ForumGroupID FROM DELETED)
END


