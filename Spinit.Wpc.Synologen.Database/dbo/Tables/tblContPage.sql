CREATE TABLE [dbo].[tblContPage] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cPgeTpeId]    INT            NOT NULL,
    [cName]        NVARCHAR (255) NOT NULL,
    [cSize]        BIGINT         NOT NULL,
    [cContent]     NTEXT          NOT NULL,
    [cCreatedBy]   NVARCHAR (100) NOT NULL,
    [cCreatedDate] SMALLDATETIME  NOT NULL,
    [cChangedBy]   NVARCHAR (100) NULL,
    [cChangedDate] SMALLDATETIME  NULL,
    CONSTRAINT [PK_tblContPage] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblContPage_tblContPageType] FOREIGN KEY ([cPgeTpeId]) REFERENCES [dbo].[tblContPageType] ([cId])
);

