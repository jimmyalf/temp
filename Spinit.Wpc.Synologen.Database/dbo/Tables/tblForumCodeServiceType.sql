CREATE TABLE [dbo].[tblForumCodeServiceType] (
    [ServiceTypeCode]        INT            IDENTITY (1, 1) NOT NULL,
    [ServiceTypeDescription] NVARCHAR (128) NULL,
    CONSTRAINT [PK_SERVICE_TYPE_CODE] PRIMARY KEY CLUSTERED ([ServiceTypeCode] ASC)
);

