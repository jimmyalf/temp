CREATE TABLE [dbo].[tblCommerceAttributeCategory] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cName]        NVARCHAR (512) NOT NULL,
    [cDescription] NTEXT          NULL,
    [cCreatedBy]   NVARCHAR (100) NOT NULL,
    [cCreatedDate] DATETIME       NOT NULL,
    [cChangedBy]   NVARCHAR (100) NULL,
    [cChangedDate] DATETIME       NULL,
    CONSTRAINT [PK_tblCommerceAttributeCategory] PRIMARY KEY CLUSTERED ([cId] ASC)
);

