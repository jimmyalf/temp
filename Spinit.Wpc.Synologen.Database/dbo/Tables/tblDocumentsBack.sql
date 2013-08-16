CREATE TABLE [dbo].[tblDocumentsBack] (
    [cId]          INT            NOT NULL,
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
    [cOrder]       INT            NULL
);

