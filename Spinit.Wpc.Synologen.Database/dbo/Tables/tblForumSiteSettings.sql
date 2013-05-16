CREATE TABLE [dbo].[tblForumSiteSettings] (
    [SiteID]         INT            IDENTITY (1, 1) NOT NULL,
    [Application]    NVARCHAR (512) CONSTRAINT [DF_forums_SiteSettings_ForumsDirectory] DEFAULT (N'/AspNetForums') NOT NULL,
    [ForumsDisabled] BIT            CONSTRAINT [DF_forums_SiteSettings_Enabled] DEFAULT (0) NOT NULL,
    [Settings]       IMAGE          CONSTRAINT [DF_forums_SiteSettings_Settings] DEFAULT ('') NOT NULL,
    [ForumsVersion]  NVARCHAR (64)  NULL,
    CONSTRAINT [PK_forums_SiteSettings] PRIMARY KEY CLUSTERED ([SiteID] ASC)
);

