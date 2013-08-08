CREATE TABLE [dbo].[tblForumEmailQueue] (
    [EmailID]          UNIQUEIDENTIFIER CONSTRAINT [DF_forums_EmailQueue_EmailID] DEFAULT (newid()) NOT NULL,
    [emailPriority]    INT              NOT NULL,
    [emailBodyFormat]  BIT              NOT NULL,
    [emailTo]          NVARCHAR (2000)  NOT NULL,
    [emailCc]          NTEXT            NULL,
    [emailBcc]         NVARCHAR (2000)  NULL,
    [EmailFrom]        NVARCHAR (256)   NOT NULL,
    [EmailSubject]     NVARCHAR (1024)  NOT NULL,
    [EmailBody]        NTEXT            NOT NULL,
    [CreatedTimestamp] DATETIME         CONSTRAINT [DF_forums_EmailQueue_createdTimestamp] DEFAULT (getdate()) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_forums_EmailQueue]
    ON [dbo].[tblForumEmailQueue]([EmailID] ASC);

