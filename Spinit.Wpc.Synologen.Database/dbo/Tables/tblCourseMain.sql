CREATE TABLE [dbo].[tblCourseMain] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (250) NOT NULL,
    [cDescription] NTEXT          NULL,
    [cDetail]      NTEXT          NULL,
    CONSTRAINT [PK_tblCoursesMain] PRIMARY KEY CLUSTERED ([cId] ASC)
);

