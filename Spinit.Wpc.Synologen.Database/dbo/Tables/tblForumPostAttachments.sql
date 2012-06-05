CREATE TABLE [dbo].[tblForumPostAttachments] (
    [PostID]      INT            NOT NULL,
    [ForumID]     INT            NOT NULL,
    [UserID]      INT            CONSTRAINT [DF_forums_PostAttachments_UserID] DEFAULT (0) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_forums_PostAttachments_Created] DEFAULT (getdate()) NOT NULL,
    [FileName]    NVARCHAR (256) NOT NULL,
    [Content]     IMAGE          NOT NULL,
    [ContentType] NVARCHAR (50)  NOT NULL,
    [ContentSize] INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_PostAttachments]
    ON [dbo].[tblForumPostAttachments]([PostID] ASC);

