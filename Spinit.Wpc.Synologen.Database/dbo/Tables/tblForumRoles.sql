CREATE TABLE [dbo].[tblForumRoles] (
    [RoleID]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (512) NOT NULL,
    CONSTRAINT [IX_forums_Roles] UNIQUE CLUSTERED ([RoleID] ASC)
);

