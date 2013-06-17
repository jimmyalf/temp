ALTER TABLE [dbo].[tblForumUsers]
    ADD CONSTRAINT [DF_forums_Users_Salt] DEFAULT ('') FOR [Salt];

