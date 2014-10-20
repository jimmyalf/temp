CREATE TABLE [dbo].[SynologenOpqFileCategories] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (512) NOT NULL,
    [IsActive]      BIT            NOT NULL,
    [CreatedById]   INT            NOT NULL,
    [CreatedByName] NVARCHAR (100) NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [ChangedById]   INT            NULL,
    [ChangedByName] NVARCHAR (100) NULL,
    [ChangedDate]   DATETIME       NULL,
    CONSTRAINT [SynologenOpqFileCategories_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [tblBaseUsers_SynologenOpqFileCategories_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqFileCategories_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);

