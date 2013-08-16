CREATE TABLE [dbo].[tblMemberPageConnection] (
    [cMemberId] INT NOT NULL,
    [cPageId]   INT NOT NULL,
    CONSTRAINT [PK_tblMemberPage] PRIMARY KEY CLUSTERED ([cMemberId] ASC, [cPageId] ASC)
);

