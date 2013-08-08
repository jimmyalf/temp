CREATE TABLE [dbo].[tblMemberFileConnection] (
    [cMemberId] INT NOT NULL,
    [cFileId]   INT NOT NULL,
    CONSTRAINT [PK_tblMemberFile] PRIMARY KEY CLUSTERED ([cMemberId] ASC, [cFileId] ASC)
);

