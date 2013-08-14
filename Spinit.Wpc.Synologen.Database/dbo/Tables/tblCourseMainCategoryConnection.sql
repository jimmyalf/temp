CREATE TABLE [dbo].[tblCourseMainCategoryConnection] (
    [cCategoryId]   INT NOT NULL,
    [cCourseMainId] INT NOT NULL,
    CONSTRAINT [PK_tblCoursesCoursesCategory] PRIMARY KEY CLUSTERED ([cCategoryId] ASC, [cCourseMainId] ASC),
    CONSTRAINT [FK_tblCoursesCoursesCategory_tblCoursesCategory] FOREIGN KEY ([cCategoryId]) REFERENCES [dbo].[tblCourseCategory] ([cId]),
    CONSTRAINT [FK_tblCoursesCoursesCategory_tblCoursesMain] FOREIGN KEY ([cCourseMainId]) REFERENCES [dbo].[tblCourseMain] ([cId])
);

