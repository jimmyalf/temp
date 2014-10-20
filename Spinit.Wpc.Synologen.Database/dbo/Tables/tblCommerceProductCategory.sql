CREATE TABLE [dbo].[tblCommerceProductCategory] (
    [cId]           INT            IDENTITY (1, 1) NOT NULL,
    [cOrder]        INT            NOT NULL,
    [cParent]       INT            NULL,
    [cName]         NVARCHAR (512) NOT NULL,
    [cDescription]  NTEXT          NULL,
    [cPicture]      INT            NULL,
    [cActive]       BIT            NOT NULL,
    [cApprovedBy]   NVARCHAR (100) NULL,
    [cApprovedDate] DATETIME       NULL,
    [cLockedBy]     NVARCHAR (100) NULL,
    [cLockedDate]   DATETIME       NULL,
    [cCreatedBy]    NVARCHAR (100) NOT NULL,
    [cCreatedDate]  DATETIME       NOT NULL,
    [cChangedBy]    NVARCHAR (100) NULL,
    [cChangedDate]  DATETIME       NULL,
    CONSTRAINT [PK_tblCommerceProductCategory] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCommerceProductCategory_tblBaseFile] FOREIGN KEY ([cPicture]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCommerceProductCategory_tblCommerceProductCategory] FOREIGN KEY ([cParent]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);

