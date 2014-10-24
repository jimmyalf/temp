CREATE TABLE [dbo].[tblForumReports] (
    [ReportID]      INT            IDENTITY (1, 1) NOT NULL,
    [ReportName]    VARCHAR (20)   NOT NULL,
    [Active]        BIT            NOT NULL,
    [ReportCommand] VARCHAR (6500) NULL,
    [ReportScript]  TEXT           NULL,
    CONSTRAINT [PK_REPORTS] PRIMARY KEY CLUSTERED ([ReportID] ASC)
);

