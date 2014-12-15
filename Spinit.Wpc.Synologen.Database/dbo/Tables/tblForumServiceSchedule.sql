CREATE TABLE [dbo].[tblForumServiceSchedule] (
    [ServiceID]         INT           NOT NULL,
    [ScheduleName]      VARCHAR (30)  NOT NULL,
    [MachineName]       VARCHAR (128) NOT NULL,
    [StartDate]         DATETIME      NOT NULL,
    [EndDate]           DATETIME      NULL,
    [ServiceParameters] VARCHAR (256) NULL,
    [ScheduleTypeCode]  INT           NOT NULL,
    [RunTimeHour]       INT           NOT NULL,
    [RunTimeMinute]     INT           NOT NULL,
    [DelayHour]         INT           NULL,
    [DelayMinute]       INT           NULL,
    [RunDaily]          BINARY (8)    NULL,
    [RunWeekly]         BINARY (8)    NULL,
    [RunMonthly]        BINARY (12)   NULL,
    [RunYearly]         TINYINT       NULL,
    [RunOnce]           TINYINT       NULL,
    [LastRunTime]       DATETIME      NULL,
    [NextRunTime]       DATETIME      NULL,
    CONSTRAINT [PK_SERVICE_SCHEDULE] PRIMARY KEY CLUSTERED ([ServiceID] ASC),
    CONSTRAINT [FK_SCHEDULE_TYPE_CD] FOREIGN KEY ([ScheduleTypeCode]) REFERENCES [dbo].[tblForumCodeScheduleType] ([ScheduleTypeCode]),
    CONSTRAINT [FK_SERVICE_ID] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[tblForumServices] ([ServiceID])
);

