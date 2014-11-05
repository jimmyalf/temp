ALTER TABLE [dbo].[tblForumUsers]
    ADD CONSTRAINT [DF_forums_Users_PasswordFormat] DEFAULT (0) FOR [PasswordFormat];

