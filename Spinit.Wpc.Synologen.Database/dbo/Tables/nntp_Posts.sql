CREATE TABLE [dbo].[nntp_Posts] (
    [PostID]       INT            CONSTRAINT [DF_nntp_Posts_PostID] DEFAULT (0) NOT NULL,
    [ForumID]      INT            CONSTRAINT [DF_nntp_Posts_ForumID] DEFAULT (0) NOT NULL,
    [NntpPostID]   INT            NOT NULL,
    [NntpUniqueID] NVARCHAR (256) NOT NULL,
    CONSTRAINT [FK_nntp_Posts_forums_Posts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[tblForumPosts] ([PostID]) ON DELETE CASCADE
);


GO


CREATE TRIGGER nntp_Post_Delete ON [nntp_Posts] 
FOR DELETE 
AS
BEGIN
	DELETE forums_Posts WHERE PostID IN (SELECT PostID FROM DELETED)
END


