CREATE TABLE [dbo].[tblContCrossPublish] (
    [cTreId]    INT NOT NULL,
    [cTreCrsId] INT NOT NULL,
    CONSTRAINT [PK_tblContCrossPublish] PRIMARY KEY CLUSTERED ([cTreId] ASC, [cTreCrsId] ASC),
    CONSTRAINT [FK_tblContCrossPublish_tblContTree] FOREIGN KEY ([cTreId]) REFERENCES [dbo].[tblContTree] ([cId]),
    CONSTRAINT [FK_tblContCrossPublish_tblContTree1] FOREIGN KEY ([cTreCrsId]) REFERENCES [dbo].[tblContTree] ([cId])
);

