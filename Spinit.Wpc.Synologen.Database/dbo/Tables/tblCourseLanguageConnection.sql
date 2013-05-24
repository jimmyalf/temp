CREATE TABLE [dbo].[tblCourseLanguageConnection] (
    [cCourseId]   INT NOT NULL,
    [cLanguageId] INT NOT NULL,
    CONSTRAINT [PK_tblCoursesLanguage] PRIMARY KEY CLUSTERED ([cCourseId] ASC, [cLanguageId] ASC),
    CONSTRAINT [FK_tblCoursesLanguage_tblBaseLanguages] FOREIGN KEY ([cLanguageId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblCoursesLanguage_tblCourses] FOREIGN KEY ([cCourseId]) REFERENCES [dbo].[tblCourse] ([cId])
);

