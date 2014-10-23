CREATE TABLE [dbo].[tblSynologenArticle] (
    [cId]                       INT            IDENTITY (1, 1) NOT NULL,
    [cArticleNumber]            NVARCHAR (50)  NOT NULL,
    [cName]                     NVARCHAR (50)  NOT NULL,
    [cDescription]              NVARCHAR (255) NULL,
    [cDefaultSPCSAccountNumber] NVARCHAR (25)  NULL,
    CONSTRAINT [PK_tblSynologenArticles] PRIMARY KEY CLUSTERED ([cId] ASC)
);

