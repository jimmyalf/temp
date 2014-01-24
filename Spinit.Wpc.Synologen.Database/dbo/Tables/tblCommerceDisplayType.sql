CREATE TABLE [dbo].[tblCommerceDisplayType] (
    [cId]   INT            IDENTITY (1, 1) NOT NULL,
    [cName] NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_tblCommerceDisplayType] PRIMARY KEY CLUSTERED ([cId] ASC)
);

