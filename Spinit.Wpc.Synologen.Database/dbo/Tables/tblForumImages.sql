CREATE TABLE [dbo].[tblForumImages] (
    [ImageID]         INT           IDENTITY (1, 1) NOT NULL,
    [Length]          INT           NOT NULL,
    [ContentType]     NVARCHAR (64) NOT NULL,
    [Content]         IMAGE         NOT NULL,
    [DateLastUpdated] DATETIME      CONSTRAINT [DF_forums_Images_DateLastUpdated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_forums_Images] PRIMARY KEY CLUSTERED ([ImageID] ASC)
);

