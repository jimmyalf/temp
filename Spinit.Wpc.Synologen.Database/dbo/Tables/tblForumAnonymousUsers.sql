CREATE TABLE [dbo].[tblForumAnonymousUsers] (
    [UserID]     CHAR (36)       NOT NULL,
    [LastLogin]  DATETIME        CONSTRAINT [DF_AnonymousUsers_LastLogin] DEFAULT (getdate()) NOT NULL,
    [LastAction] NVARCHAR (1024) CONSTRAINT [DF_forums_AnonymousUsers_LastAction] DEFAULT ('') NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AnonymousUsers]
    ON [dbo].[tblForumAnonymousUsers]([LastLogin] ASC);

