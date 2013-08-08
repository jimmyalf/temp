CREATE TABLE [dbo].[tblForumUsersOnline] (
    [UserID]       INT             CONSTRAINT [DF_forums_UsersOnline_UserID] DEFAULT (0) NOT NULL,
    [LastActivity] DATETIME        NOT NULL,
    [LastAction]   NVARCHAR (1024) CONSTRAINT [DF_forums_UsersOnline_LastAction] DEFAULT ('') NOT NULL,
    CONSTRAINT [IX_forums_UsersOnline] UNIQUE NONCLUSTERED ([UserID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_UsersOnline_1]
    ON [dbo].[tblForumUsersOnline]([LastActivity] ASC);

