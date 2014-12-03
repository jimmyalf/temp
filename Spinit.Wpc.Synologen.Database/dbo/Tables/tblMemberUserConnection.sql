CREATE TABLE [dbo].[tblMemberUserConnection] (
    [cMemberId] INT NOT NULL,
    [cUserId]   INT NOT NULL,
    CONSTRAINT [PK_tblMemberUserConnection] PRIMARY KEY CLUSTERED ([cMemberId] ASC, [cUserId] ASC),
    CONSTRAINT [FK_tblMemberUserConnection_tblBaseUsers] FOREIGN KEY ([cUserId]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [FK_tblMemberUserConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId])
);

