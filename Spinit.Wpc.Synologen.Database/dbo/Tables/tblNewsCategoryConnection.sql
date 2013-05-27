CREATE TABLE [dbo].[tblNewsCategoryConnection] (
    [cCategoryId] INT NOT NULL,
    [cNewsId]     INT NOT NULL,
    CONSTRAINT [PK_tblNewsCategoryConnection] PRIMARY KEY CLUSTERED ([cCategoryId] ASC, [cNewsId] ASC),
    CONSTRAINT [FK_tblNewsCatLocConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId]),
    CONSTRAINT [FK_tblNewsCatLocConnection_tblNewsCat] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblNewsCategory] ([cId])
);

