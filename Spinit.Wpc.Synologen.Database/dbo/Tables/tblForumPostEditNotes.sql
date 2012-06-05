CREATE TABLE [dbo].[tblForumPostEditNotes] (
    [PostID]    INT             NOT NULL,
    [EditNotes] NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_forums_PostEditNotes] PRIMARY KEY NONCLUSTERED ([PostID] ASC),
    CONSTRAINT [FK_forums_PostEditNotes_forums_Posts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[tblForumPosts] ([PostID]) ON DELETE CASCADE,
    CONSTRAINT [IX_forums_PostEditNotes] UNIQUE CLUSTERED ([PostID] DESC)
);

