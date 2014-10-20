CREATE TABLE [dbo].[tblQuickFormInbox] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cQuickFormId] INT            NOT NULL,
    [cRead]        BIT            NOT NULL,
    [cContent]     NTEXT          NOT NULL,
    [cFrom]        NVARCHAR (255) NULL,
    [cSubmitDate]  DATETIME       NOT NULL,
    CONSTRAINT [PK_tblQuickFormInbox] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblQuickFormInbox_tblQuickForm] FOREIGN KEY ([cQuickFormId]) REFERENCES [dbo].[tblQuickForm] ([cId])
);

