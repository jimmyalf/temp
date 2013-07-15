CREATE TABLE [dbo].[tblNewsCategory] (
    [cId]         INT IDENTITY (1, 1) NOT NULL,
    [cResourceId] INT NOT NULL,
    [cOrder]      INT NOT NULL,
    CONSTRAINT [PK_tblNewsCat] PRIMARY KEY CLUSTERED ([cId] ASC)
);

