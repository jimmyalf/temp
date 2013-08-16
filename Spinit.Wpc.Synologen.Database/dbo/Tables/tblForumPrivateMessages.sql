CREATE TABLE [dbo].[tblForumPrivateMessages] (
    [UserID]   INT NOT NULL,
    [ThreadID] INT NOT NULL
);


GO
CREATE CLUSTERED INDEX [IX_forums_PrivateMessages]
    ON [dbo].[tblForumPrivateMessages]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_PrivateMessages_1]
    ON [dbo].[tblForumPrivateMessages]([ThreadID] ASC);

