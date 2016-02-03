ALTER TABLE [dbo].[tblForumUsers]
    ADD CONSTRAINT [DF_forums_Users_PasswordQuestion] DEFAULT ('') FOR [PasswordQuestion];

