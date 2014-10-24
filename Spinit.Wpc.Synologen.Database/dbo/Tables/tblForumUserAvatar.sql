CREATE TABLE [dbo].[tblForumUserAvatar] (
    [UserID]  INT NOT NULL,
    [ImageID] INT NOT NULL,
    CONSTRAINT [FK_forums_UserAvatar_forums_Images] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[tblForumImages] ([ImageID]) ON DELETE CASCADE,
    CONSTRAINT [FK_forums_UserAvatar_forums_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblForumUsers] ([UserID]) ON DELETE CASCADE
);


GO
CREATE CLUSTERED INDEX [IX_forums_UserAvatar_1]
    ON [dbo].[tblForumUserAvatar]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_UserAvatar]
    ON [dbo].[tblForumUserAvatar]([UserID] ASC);

