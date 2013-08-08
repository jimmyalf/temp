CREATE TABLE [dbo].[tblNewsRead] (
    [cNewsId]   INT            NOT NULL,
    [cReadBy]   NVARCHAR (100) NOT NULL,
    [cReadDate] DATETIME       NULL,
    CONSTRAINT [PK_tblNewsRead] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cReadBy] ASC)
);

