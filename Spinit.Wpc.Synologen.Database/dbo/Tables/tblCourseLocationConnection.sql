CREATE TABLE [dbo].[tblCourseLocationConnection] (
    [cCourseId]   INT NOT NULL,
    [cLocationId] INT NOT NULL,
    CONSTRAINT [PK_tblCoursesLocation] PRIMARY KEY CLUSTERED ([cCourseId] ASC, [cLocationId] ASC),
    CONSTRAINT [FK_tblCoursesLocation_tblBaseLocations] FOREIGN KEY ([cLocationId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblCoursesLocation_tblCourses] FOREIGN KEY ([cCourseId]) REFERENCES [dbo].[tblCourse] ([cId])
);

