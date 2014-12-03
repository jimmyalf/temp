CREATE TABLE [dbo].[tblMemberLocationConnection] (
    [cLocationId] INT NOT NULL,
    [cMemberId]   INT NOT NULL,
    CONSTRAINT [PK_tblMemberLocationConnection] PRIMARY KEY CLUSTERED ([cLocationId] ASC, [cMemberId] ASC),
    CONSTRAINT [FK_tblMemberLocationConnection_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId])
);

