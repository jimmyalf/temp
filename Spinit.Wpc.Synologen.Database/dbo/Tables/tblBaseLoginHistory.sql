CREATE TABLE [dbo].[tblBaseLoginHistory] (
    [cId]         INT            IDENTITY (1, 1) NOT NULL,
    [cSuccess]    BIT            NOT NULL,
    [cLoginTime]  DATETIME       NOT NULL,
    [cLogoutTime] DATETIME       NULL,
    [cUsrId]      INT            NULL,
    [cName]       NVARCHAR (100) NULL,
    [cPassword]   NVARCHAR (100) NULL,
    [cIpNumber]   VARCHAR (50)   NOT NULL,
    [cUserAgent]  NVARCHAR (512) NOT NULL,
    [cSession]    NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_tblBaseLoginHistory] PRIMARY KEY CLUSTERED ([cId] ASC)
);

