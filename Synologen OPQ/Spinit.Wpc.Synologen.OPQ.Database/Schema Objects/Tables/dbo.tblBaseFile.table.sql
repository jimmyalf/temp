CREATE TABLE [dbo].[tblBaseFile] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NTEXT          NOT NULL,
    [cDirectory]   BIT            NULL,
    [cContentInfo] NVARCHAR (256) NULL,
    [cKeyWords]    NVARCHAR (256) NULL,
    [cDescription] NVARCHAR (256) NULL,
    [cCreatedBy]   NVARCHAR (100) NOT NULL,
    [cCreatedDate] SMALLDATETIME  NOT NULL,
    [cChangedBy]   NVARCHAR (100) NULL,
    [cChangedDate] SMALLDATETIME  NULL,
    [cIconType]    INT            NULL
);

