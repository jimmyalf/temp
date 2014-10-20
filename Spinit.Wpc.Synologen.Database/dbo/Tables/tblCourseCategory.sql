CREATE TABLE [dbo].[tblCourseCategory] (
    [cId]         INT IDENTITY (1, 1) NOT NULL,
    [cResourceId] INT NOT NULL,
    [cOrder]      INT NULL,
    CONSTRAINT [PK_tblCoursesCategory] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCourseCategory_tblCourseResource] FOREIGN KEY ([cId]) REFERENCES [dbo].[tblCourseCategory] ([cId])
);

