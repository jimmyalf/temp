CREATE TABLE [dbo].[tblForumUsers] (
    [UserID]            INT             IDENTITY (1, 1) NOT NULL,
    [UserName]          NVARCHAR (64)   NOT NULL,
    [Password]          NVARCHAR (64)   NOT NULL,
    [PasswordFormat]    SMALLINT        CONSTRAINT [DF_forums_Users_PasswordFormat] DEFAULT (0) NOT NULL,
    [Salt]              VARCHAR (24)    CONSTRAINT [DF_forums_Users_Salt] DEFAULT ('') NOT NULL,
    [PasswordQuestion]  NVARCHAR (256)  CONSTRAINT [DF_forums_Users_PasswordQuestion] DEFAULT ('') NULL,
    [PasswordAnswer]    NVARCHAR (256)  NULL,
    [Email]             NVARCHAR (128)  NOT NULL,
    [DateCreated]       DATETIME        CONSTRAINT [DF_Users_DateCreated] DEFAULT (getdate()) NOT NULL,
    [LastLogin]         DATETIME        CONSTRAINT [DF_Users_LastLogin] DEFAULT (getdate()) NOT NULL,
    [LastActivity]      DATETIME        CONSTRAINT [DF_Users_LastActivity] DEFAULT (getdate()) NOT NULL,
    [LastAction]        NVARCHAR (1024) CONSTRAINT [DF_forums_Users_LastAction] DEFAULT ('') NOT NULL,
    [UserAccountStatus] SMALLINT        CONSTRAINT [DF_Users_Approved] DEFAULT (1) NOT NULL,
    [IsAnonymous]       BIT             CONSTRAINT [DF_forums_Users_IsAnonymous] DEFAULT (0) NOT NULL,
    [ForceLogin]        BIT             CONSTRAINT [DF_forums_Users_ForcLogin] DEFAULT (0) NOT NULL,
    [AppUserToken]      VARCHAR (128)   NULL,
    CONSTRAINT [PK_forums_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [IX_forums_Users_2] UNIQUE NONCLUSTERED ([UserName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[tblForumUsers]([DateCreated] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_Users]
    ON [dbo].[tblForumUsers]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_forums_Users_1]
    ON [dbo].[tblForumUsers]([Email] ASC);

