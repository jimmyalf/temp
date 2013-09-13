CREATE TABLE [dbo].[tblForumBlockedIpAddresses] (
    [IpID]    INT            IDENTITY (1, 1) NOT NULL,
    [Address] NVARCHAR (50)  NULL,
    [Reason]  NVARCHAR (512) NULL,
    CONSTRAINT [PK_forums_BlockedIpAddresses] PRIMARY KEY CLUSTERED ([IpID] ASC)
);

