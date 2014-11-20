CREATE TABLE [dbo].[tblNewsFileConnection] (
    [cNewsId] INT NOT NULL,
    [cFileId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsFile] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblNewsFileConnection_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblNewsFileConnection_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

