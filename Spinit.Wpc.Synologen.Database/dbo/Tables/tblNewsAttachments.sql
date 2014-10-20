CREATE TABLE [dbo].[tblNewsAttachments] (
    [cNewsId] INT NOT NULL,
    [cFileId] INT NOT NULL,
    CONSTRAINT [PK_tblNewsAttachments] PRIMARY KEY CLUSTERED ([cNewsId] ASC, [cFileId] ASC),
    CONSTRAINT [FK_tblNewsAttachments_tblBaseFile] FOREIGN KEY ([cFileId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblNewsAttachments_tblNews] FOREIGN KEY ([cNewsId]) REFERENCES [dbo].[tblNews] ([cId])
);

