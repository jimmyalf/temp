CREATE TABLE [dbo].[tblCourseApplication] (
    [cId]               INT             IDENTITY (1, 1) NOT NULL,
    [cCourseId]         INT             NULL,
    [cFirstName]        NVARCHAR (30)   NULL,
    [cLastName]         NVARCHAR (30)   NULL,
    [cCompany]          NVARCHAR (50)   NULL,
    [cOrgNr]            NVARCHAR (50)   NULL,
    [cAddress]          NVARCHAR (50)   NULL,
    [cPostCode]         NVARCHAR (10)   NULL,
    [cCity]             NVARCHAR (50)   NULL,
    [cCountry]          NVARCHAR (50)   NULL,
    [cPhone]            NVARCHAR (15)   NULL,
    [cMobile]           NVARCHAR (15)   NULL,
    [cEmail]            NVARCHAR (30)   NULL,
    [cApplicationText]  NVARCHAR (2000) NULL,
    [cNrOfParticipants] INT             NULL,
    [cCreatedDate]      SMALLDATETIME   NULL,
    CONSTRAINT [PK_tblCourseApplications] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCourseApplications_tblCourses] FOREIGN KEY ([cCourseId]) REFERENCES [dbo].[tblCourse] ([cId])
);

