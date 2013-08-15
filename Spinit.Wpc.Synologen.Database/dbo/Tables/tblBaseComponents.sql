CREATE TABLE [dbo].[tblBaseComponents] (
    [cId]                INT            IDENTITY (1, 1) NOT NULL,
    [cName]              NVARCHAR (256) NOT NULL,
    [cCompInstanceTable] NVARCHAR (256) NOT NULL,
    [cDescription]       NVARCHAR (256) NULL,
    [cFromComponentList] BIT            NULL,
    [cFromWysiwyg]       BIT            NULL,
    [cFramePage]         NVARCHAR (256) NULL,
    [cEditPage]          NVARCHAR (256) NULL,
    [cExternal]          BIT            NULL,
    CONSTRAINT [PK_tblBaseComponents] PRIMARY KEY CLUSTERED ([cId] ASC)
);

