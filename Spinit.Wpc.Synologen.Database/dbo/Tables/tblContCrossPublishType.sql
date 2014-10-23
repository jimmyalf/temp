CREATE TABLE [dbo].[tblContCrossPublishType] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NOT NULL,
    [cDescription] NVARCHAR (256) NULL,
    CONSTRAINT [PK_tblContCrossPublishType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

