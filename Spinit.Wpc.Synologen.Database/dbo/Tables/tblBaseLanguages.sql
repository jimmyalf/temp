CREATE TABLE [dbo].[tblBaseLanguages] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (255) NOT NULL,
    [cDescription] NVARCHAR (256) NULL,
    [cResource]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tblBaseLanguage] PRIMARY KEY CLUSTERED ([cId] ASC)
);

