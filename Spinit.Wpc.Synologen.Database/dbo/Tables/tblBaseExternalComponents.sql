CREATE TABLE [dbo].[tblBaseExternalComponents] (
    [cComponentId]    INT            NOT NULL,
    [cAdminLink]      NVARCHAR (500) NULL,
    [cAdminTarget]    NVARCHAR (256) NULL,
    [cPublicLink]     NVARCHAR (500) NULL,
    [cPublicTarget]   NVARCHAR (256) NULL,
    [cWinLogin]       NVARCHAR (256) NULL,
    [cWinPassword]    NVARCHAR (256) NULL,
    [cWinCookie]      BIT            NULL,
    [cCommonLogin]    NVARCHAR (256) NULL,
    [cCommonPassword] NVARCHAR (256) NULL,
    [cCommonCookie]   BIT            NULL,
    CONSTRAINT [PK_tblBaseExternalComponents] PRIMARY KEY CLUSTERED ([cComponentId] ASC),
    CONSTRAINT [FK_tblBaseExternalComponents_tblBaseComponents] FOREIGN KEY ([cComponentId]) REFERENCES [dbo].[tblBaseComponents] ([cId])
);

