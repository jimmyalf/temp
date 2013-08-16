CREATE TABLE [dbo].[tblBaseObjects] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (50)  NOT NULL,
    [cDescription] NVARCHAR (512) NULL,
    [cCmpId]       INT            NOT NULL,
    CONSTRAINT [PK_tblBaseObjects] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblBaseObjects_tblBaseComponents] FOREIGN KEY ([cCmpId]) REFERENCES [dbo].[tblBaseComponents] ([cId])
);

