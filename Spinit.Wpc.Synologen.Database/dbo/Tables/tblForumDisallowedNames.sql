CREATE TABLE [dbo].[tblForumDisallowedNames] (
    [DisallowedName] NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_DISALLOWED_NAME] PRIMARY KEY CLUSTERED ([DisallowedName] ASC)
);

