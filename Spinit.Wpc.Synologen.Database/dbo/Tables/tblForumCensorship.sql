CREATE TABLE [dbo].[tblForumCensorship] (
    [Word]        NVARCHAR (20) NOT NULL,
    [Replacement] NVARCHAR (20) NULL,
    CONSTRAINT [PK_CENSORSHIP] PRIMARY KEY CLUSTERED ([Word] ASC)
);

