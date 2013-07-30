CREATE TABLE [dbo].[tblForumUsers] (
    [UserID]            INT             IDENTITY (1, 1) NOT NULL,
    [UserName]          NVARCHAR (64)   NOT NULL,
    [Password]          NVARCHAR (64)   NOT NULL,
    [PasswordFormat]    SMALLINT        NOT NULL,
    [Salt]              VARCHAR (24)    NOT NULL,
    [PasswordQuestion]  NVARCHAR (256)  NULL,
    [PasswordAnswer]    NVARCHAR (256)  NULL,
    [Email]             NVARCHAR (128)  NOT NULL,
    [DateCreated]       DATETIME        NOT NULL,
    [LastLogin]         DATETIME        NOT NULL,
    [LastActivity]      DATETIME        NOT NULL,
    [LastAction]        NVARCHAR (1024) NOT NULL,
    [UserAccountStatus] SMALLINT        NOT NULL,
    [IsAnonymous]       BIT             NOT NULL,
    [ForceLogin]        BIT             NOT NULL,
    [AppUserToken]      VARCHAR (128)   NULL
);

