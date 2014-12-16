CREATE TABLE [dbo].[tblForumCodeScheduleType] (
    [ScheduleTypeCode]    INT          IDENTITY (1, 1) NOT NULL,
    [ScheduleDescription] VARCHAR (30) NULL,
    CONSTRAINT [PK_CODE_SCHEDULE_TYPE] PRIMARY KEY CLUSTERED ([ScheduleTypeCode] ASC)
);

