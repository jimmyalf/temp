ALTER TABLE [dbo].[tblForumUsers]
    ADD CONSTRAINT [DF_Users_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

