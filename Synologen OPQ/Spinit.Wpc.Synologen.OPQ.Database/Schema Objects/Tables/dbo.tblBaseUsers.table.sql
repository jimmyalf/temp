CREATE TABLE [dbo].[tblBaseUsers] (
    [cId]              INT            IDENTITY (1, 1) NOT NULL,
    [cUserName]        NVARCHAR (100) NOT NULL,
    [cPassword]        NVARCHAR (100) NOT NULL,
    [cFirstName]       NVARCHAR (100) NULL,
    [cLastName]        NVARCHAR (100) NULL,
    [cEmail]           NVARCHAR (512) NULL,
    [cDefaultLocation] INT            NULL,
    [cActive]          BIT            NOT NULL,
    [cCreatedBy]       NVARCHAR (100) NULL,
    [cCreatedDate]     DATETIME       NULL,
    [cChangedBy]       NVARCHAR (100) NULL,
    [cChangedDate]     DATETIME       NULL
);

