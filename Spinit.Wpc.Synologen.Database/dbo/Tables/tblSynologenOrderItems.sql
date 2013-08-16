CREATE TABLE [dbo].[tblSynologenOrderItems] (
    [cId]                INT            IDENTITY (1, 1) NOT NULL,
    [cOrderId]           INT            NOT NULL,
    [cArticleId]         INT            NOT NULL,
    [cSinglePrice]       FLOAT (53)     NOT NULL,
    [cNoVAT]             BIT            CONSTRAINT [DF_tblSynologenOrderItems_cNoVat] DEFAULT (0) NOT NULL,
    [cNumberOfItems]     INT            NOT NULL,
    [cNotes]             NVARCHAR (255) NULL,
    [cSPCSAccountNumber] NVARCHAR (50)  CONSTRAINT [DF_tblSynologenOrderItems_cSPCSAccountNumber] DEFAULT (N'3071') NOT NULL,
    CONSTRAINT [PK_tblSynologenOrderItems] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblSynologenOrderItems_tblSynologenArticles] FOREIGN KEY ([cArticleId]) REFERENCES [dbo].[tblSynologenArticle] ([cId]),
    CONSTRAINT [FK_tblSynologenOrderItems_tblSynologenOrder] FOREIGN KEY ([cOrderId]) REFERENCES [dbo].[tblSynologenOrder] ([cId])
);

