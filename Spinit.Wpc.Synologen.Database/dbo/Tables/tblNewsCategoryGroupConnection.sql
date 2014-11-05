CREATE TABLE [dbo].[tblNewsCategoryGroupConnection] (
    [cCategoryId] INT NOT NULL,
    [cGroupId]    INT NOT NULL,
    CONSTRAINT [PK_tblNewsCategoryGroupConnection] PRIMARY KEY CLUSTERED ([cCategoryId] ASC, [cGroupId] ASC),
    CONSTRAINT [FK_tblNewsCategoryGroupConnection_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblNewsCategoryGroupConnection_tblNewsCategory] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblNewsCategory] ([cId])
);

