CREATE TABLE [dbo].[tblForumUsersInRoles] (
    [UserID] INT NOT NULL,
    [RoleID] INT NOT NULL
);


GO
CREATE CLUSTERED INDEX [IX_forums_UserRoles]
    ON [dbo].[tblForumUsersInRoles]([UserID] ASC, [RoleID] ASC);

