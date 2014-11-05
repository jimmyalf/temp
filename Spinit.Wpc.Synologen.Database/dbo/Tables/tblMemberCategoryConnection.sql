CREATE TABLE [dbo].[tblMemberCategoryConnection] (
    [cMemberId]   INT NOT NULL,
    [cCategoryId] INT NOT NULL,
    CONSTRAINT [PK_tblMemberCategoryConnection] PRIMARY KEY CLUSTERED ([cMemberId] ASC, [cCategoryId] ASC),
    CONSTRAINT [FK_tblMemberCategoryConnection_tblMemberCategories] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblMemberCategories] ([cId]),
    CONSTRAINT [FK_tblMemberCategoryConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId])
);

