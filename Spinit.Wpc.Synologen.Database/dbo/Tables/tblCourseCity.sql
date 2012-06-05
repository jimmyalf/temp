CREATE TABLE [dbo].[tblCourseCity] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cCity] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblCity] PRIMARY KEY CLUSTERED ([cId] ASC)
);

