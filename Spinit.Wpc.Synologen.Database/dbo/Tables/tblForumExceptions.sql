CREATE TABLE [dbo].[tblForumExceptions] (
    [ExceptionID]      INT             IDENTITY (1, 1) NOT NULL,
    [ExceptionHash]    VARCHAR (128)   NOT NULL,
    [SiteID]           INT             NOT NULL,
    [Category]         INT             NOT NULL,
    [Exception]        NVARCHAR (2000) NOT NULL,
    [ExceptionMessage] NVARCHAR (500)  NOT NULL,
    [IPAddress]        VARCHAR (15)    NOT NULL,
    [UserAgent]        NVARCHAR (64)   NOT NULL,
    [HttpReferrer]     NVARCHAR (256)  NOT NULL,
    [HttpVerb]         NVARCHAR (24)   NOT NULL,
    [PathAndQuery]     NVARCHAR (512)  NOT NULL,
    [DateCreated]      DATETIME        CONSTRAINT [DF_forums_Exceptions_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateLastOccurred] DATETIME        NOT NULL,
    [Frequency]        INT             CONSTRAINT [DF_forums_Exceptions_Frequency] DEFAULT (0) NOT NULL,
    CONSTRAINT [IX_forums_Exceptions] UNIQUE NONCLUSTERED ([ExceptionID] ASC),
    CONSTRAINT [IX_forums_Exceptions_1] UNIQUE NONCLUSTERED ([ExceptionHash] ASC)
);

