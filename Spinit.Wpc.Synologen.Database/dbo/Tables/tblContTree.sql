CREATE TABLE [dbo].[tblContTree] (
    [cId]                  INT            IDENTITY (1, 1) NOT NULL,
    [cPgeId]               INT            NULL,
    [cLocId]               INT            NOT NULL,
    [cLngId]               INT            NULL,
    [cTreTpeId]            INT            NOT NULL,
    [cParent]              INT            NULL,
    [cName]                NVARCHAR (255) NOT NULL,
    [cFileName]            NTEXT          NOT NULL,
    [cDescription]         NTEXT          NULL,
    [cNote]                NVARCHAR (256) NULL,
    [cKeywords]            NTEXT          NULL,
    [cLink]                NVARCHAR (256) NULL,
    [cTarget]              NVARCHAR (256) NULL,
    [cHeader]              NTEXT          NULL,
    [cHideInMenu]          BIT            NULL,
    [cExcludeFromSearch]   BIT            NULL,
    [cTemplate]            INT            NULL,
    [cStylesheet]          INT            NULL,
    [cOrder]               INT            NOT NULL,
    [cCreatedBy]           NVARCHAR (100) NOT NULL,
    [cCreatedDate]         SMALLDATETIME  NOT NULL,
    [cChangedBy]           NVARCHAR (100) NULL,
    [cChangedDate]         SMALLDATETIME  NULL,
    [cApprovedBy]          NVARCHAR (100) NULL,
    [cApprovedDate]        SMALLDATETIME  NULL,
    [cPublishDate]         SMALLDATETIME  NULL,
    [cPublishedDate]       SMALLDATETIME  NULL,
    [cUnPublishDate]       SMALLDATETIME  NULL,
    [cUnpublishedDate]     SMALLDATETIME  NULL,
    [cLockedBy]            NVARCHAR (100) NULL,
    [cLockedDate]          SMALLDATETIME  NULL,
    [cCrsTpeId]            INT            NOT NULL,
    [cLockFileName]        BIT            NOT NULL,
    [cNeedsAuthentication] BIT            NOT NULL,
    [cCssClass]            NVARCHAR (255) NULL,
    [cPublish]             BIT            CONSTRAINT [DF_tblContTree_cPublish] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblContTree] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblContTree_tblBaseLanguages] FOREIGN KEY ([cLngId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblContTree_tblBaseLocations] FOREIGN KEY ([cLocId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContCrossPublishType] FOREIGN KEY ([cCrsTpeId]) REFERENCES [dbo].[tblContCrossPublishType] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContPage] FOREIGN KEY ([cPgeId]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContPage1] FOREIGN KEY ([cTemplate]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContPage2] FOREIGN KEY ([cStylesheet]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContTree] FOREIGN KEY ([cParent]) REFERENCES [dbo].[tblContTree] ([cId]),
    CONSTRAINT [FK_tblContTree_tblContTreeType] FOREIGN KEY ([cTreTpeId]) REFERENCES [dbo].[tblContTreeType] ([cId])
);


GO
create TRIGGER tblContTree_AspNet_SqlCacheNotification_Trigger ON tblContTree
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'tblContTree'
                       END
