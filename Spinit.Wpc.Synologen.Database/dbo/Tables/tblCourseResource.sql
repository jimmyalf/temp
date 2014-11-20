CREATE TABLE [dbo].[tblCourseResource] (
    [cId]         INT   NOT NULL,
    [cLanguageId] INT   NOT NULL,
    [cResource]   NTEXT NULL,
    CONSTRAINT [PK_tblCourseResource] PRIMARY KEY CLUSTERED ([cId] ASC, [cLanguageId] ASC)
);

