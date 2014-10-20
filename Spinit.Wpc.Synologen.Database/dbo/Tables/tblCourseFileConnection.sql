CREATE TABLE [dbo].[tblCourseFileConnection] (
    [cCourseId] INT NOT NULL,
    [cFileId]   INT NOT NULL,
    CONSTRAINT [PK_tblCourseFileConnection] PRIMARY KEY CLUSTERED ([cCourseId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblCourseFileConnection_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCourseFileConnection_tblCourse] FOREIGN KEY ([cCourseId]) REFERENCES [dbo].[tblCourse] ([cId])
);

