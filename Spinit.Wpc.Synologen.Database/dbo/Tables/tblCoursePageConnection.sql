CREATE TABLE [dbo].[tblCoursePageConnection] (
    [cCourseId] INT NOT NULL,
    [cPageId]   INT NOT NULL,
    CONSTRAINT [PK_tblCoursePageConnection] PRIMARY KEY CLUSTERED ([cCourseId] ASC, [cPageId] ASC),
    CONSTRAINT [FK_tblCoursePageConnection_tblContPage] FOREIGN KEY ([cPageId]) REFERENCES [dbo].[tblContPage] ([cId]),
    CONSTRAINT [FK_tblCoursePageConnection_tblCourse] FOREIGN KEY ([cCourseId]) REFERENCES [dbo].[tblCourse] ([cId])
);

