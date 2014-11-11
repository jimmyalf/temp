CREATE TABLE [dbo].[tblDocumentNode] (
    [cId]       INT            IDENTITY (1, 1) NOT NULL,
    [cParentId] INT            NULL,
    [cName]     NVARCHAR (100) NULL,
    [cGroupId]  INT            NULL,
    [cUserId]   INT            NULL,
    [cSortType] INT            NULL,
    [cOrder]    INT            NULL,
    CONSTRAINT [PK_tblDocumentNode] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblDocumentNode_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblDocumentNode_tblBaseUsers] FOREIGN KEY ([cUserId]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);

