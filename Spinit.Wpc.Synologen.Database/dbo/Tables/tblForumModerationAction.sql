CREATE TABLE [dbo].[tblForumModerationAction] (
    [ModerationAction] INT            NOT NULL,
    [Description]      NVARCHAR (128) NOT NULL,
    [TotalActions]     INT            CONSTRAINT [DF_ModerationAction_TotalActions] DEFAULT (0) NOT NULL,
    CONSTRAINT [IX_forums_ModerationAction] UNIQUE CLUSTERED ([ModerationAction] ASC)
);

