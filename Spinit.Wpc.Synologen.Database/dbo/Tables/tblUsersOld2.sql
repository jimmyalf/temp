CREATE TABLE [dbo].[tblUsersOld2] (
    [StoreID]       INT            NOT NULL,
    [StoreName]     VARCHAR (70)   NULL,
    [FName]         VARCHAR (50)   NULL,
    [LName]         VARCHAR (50)   NULL,
    [Address]       VARCHAR (100)  NULL,
    [PostCode]      VARCHAR (10)   NULL,
    [City]          VARCHAR (50)   NULL,
    [Phone]         VARCHAR (15)   NULL,
    [Fax]           VARCHAR (15)   NULL,
    [Email]         VARCHAR (70)   NULL,
    [UserName]      VARCHAR (12)   NOT NULL,
    [PassWord]      VARCHAR (8)    NOT NULL,
    [Blocked]       TINYINT        NOT NULL,
    [Usertype]      TINYINT        NULL,
    [PassUpdate]    DATETIME       NULL,
    [ContactPerson] VARCHAR (100)  NULL,
    [Homepage]      VARCHAR (100)  NULL,
    [About]         VARCHAR (4000) NULL
);

