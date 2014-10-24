CREATE TABLE [dbo].[tblDocuments] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cNodeId]      INT            NULL,
    [cName]        NVARCHAR (100) NULL,
    [cContentInfo] NVARCHAR (50)  NULL,
    [cSize]        INT            NULL,
    [cContent]     IMAGE          NULL,
    [cDescription] NVARCHAR (255) NULL,
    [cCreatedBy]   NVARCHAR (50)  NULL,
    [cCreatedDate] SMALLDATETIME  NULL,
    [cChangedBy]   NVARCHAR (50)  NULL,
    [cChangedDate] SMALLDATETIME  NULL,
    [cNoOfViews]   INT            NULL,
    [cGroupId]     INT            NULL,
    [cUserId]      INT            NULL,
    [cOrder]       INT            NULL,
    CONSTRAINT [PK_tblDocuments] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblDocuments_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblDocuments_tblBaseUsers] FOREIGN KEY ([cUserId]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [FK_tblDocuments_tblDocumentNode] FOREIGN KEY ([cNodeId]) REFERENCES [dbo].[tblDocumentNode] ([cId])
);

