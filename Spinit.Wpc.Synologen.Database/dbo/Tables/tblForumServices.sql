CREATE TABLE [dbo].[tblForumServices] (
    [ServiceID]               INT            IDENTITY (1, 1) NOT NULL,
    [ServiceName]             NVARCHAR (30)  NULL,
    [ServiceTypeCode]         INT            NULL,
    [ServiceAssemblyPath]     NVARCHAR (256) NULL,
    [ServiceFullClassName]    NVARCHAR (256) NULL,
    [ServiceWorkingDirectory] NVARCHAR (256) NULL,
    CONSTRAINT [PK_SERVICE_ID] PRIMARY KEY CLUSTERED ([ServiceID] ASC),
    CONSTRAINT [FK_SERVICE_TYPE_CODE] FOREIGN KEY ([ServiceTypeCode]) REFERENCES [dbo].[tblForumCodeServiceType] ([ServiceTypeCode]),
    CONSTRAINT [UK_SERVICE_NAME] UNIQUE NONCLUSTERED ([ServiceName] ASC)
);

