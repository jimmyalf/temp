CREATE TABLE [dbo].[tblUsersOld] (
    [ID]            INT            NULL,
    [StoreName]     NVARCHAR (255) NULL,
    [FName]         NVARCHAR (255) NULL,
    [LName]         NVARCHAR (255) NULL,
    [Address]       NVARCHAR (255) NULL,
    [PostCode]      NVARCHAR (255) NULL,
    [City]          NVARCHAR (255) NULL,
    [Phone]         NVARCHAR (255) NULL,
    [Fax]           NVARCHAR (255) NULL,
    [Email]         NVARCHAR (255) NULL,
    [UserName]      NVARCHAR (255) NULL,
    [PassWord]      NVARCHAR (255) NULL,
    [Blocked]       FLOAT (53)     NULL,
    [Usertype]      FLOAT (53)     NULL,
    [PassUpdate]    SMALLDATETIME  NULL,
    [ContactPerson] NVARCHAR (50)  NULL,
    [Homepage]      NVARCHAR (255) NULL,
    [About]         NVARCHAR (50)  NULL
);

