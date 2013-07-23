CREATE TABLE [dbo].[tblMemberClassifiedAds] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cMemberId]    INT            NULL,
    [cHeading]     NVARCHAR (100) NULL,
    [cDescription] NVARCHAR (512) NULL,
    [cStartDate]   DATETIME       NULL,
    [cEndDate]     DATETIME       NULL,
    [cActive]      BIT            NULL,
    [cName]        NVARCHAR (100) NULL,
    [cEmail]       NVARCHAR (100) NULL,
    [cTelephone]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tblMemberClassifiedAds] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblMemberClassifiedAds_tblMembers] FOREIGN KEY ([cMemberId]) REFERENCES [dbo].[tblMembers] ([cId])
);

