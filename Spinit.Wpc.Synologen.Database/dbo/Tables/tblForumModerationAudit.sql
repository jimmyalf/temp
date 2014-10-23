CREATE TABLE [dbo].[tblForumModerationAudit] (
    [ModerationAction] INT             NOT NULL,
    [PostID]           INT             NULL,
    [UserID]           INT             NULL,
    [ForumID]          INT             NULL,
    [ModeratorID]      INT             NOT NULL,
    [ModeratedOn]      DATETIME        CONSTRAINT [DF_forums_ModerationAudit_ModeratedOn] DEFAULT (getdate()) NOT NULL,
    [Notes]            NVARCHAR (1024) NULL,
    CONSTRAINT [FK_forums_ModerationAudit_forums_ModerationAction] FOREIGN KEY ([ModerationAction]) REFERENCES [dbo].[tblForumModerationAction] ([ModerationAction]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_ModerationAudit]
    ON [dbo].[tblForumModerationAudit]([ModerationAction] ASC);

