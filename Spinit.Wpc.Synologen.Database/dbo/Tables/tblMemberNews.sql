CREATE TABLE [dbo].[tblMemberNews] (
    [cMemberId] INT NOT NULL,
    [cNewsId]   INT NOT NULL,
    CONSTRAINT [PK_tblMemberNews] PRIMARY KEY CLUSTERED ([cMemberId] ASC, [cNewsId] ASC)
);

