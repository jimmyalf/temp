CREATE TABLE [dbo].[tblForumMessages] (
    [MessageID] INT             NOT NULL,
    [Language]  NVARCHAR (8)    CONSTRAINT [DF_forums_Messages_Language] DEFAULT ('en-US') NOT NULL,
    [Title]     NVARCHAR (1024) NOT NULL,
    [Body]      NTEXT           NOT NULL
);

