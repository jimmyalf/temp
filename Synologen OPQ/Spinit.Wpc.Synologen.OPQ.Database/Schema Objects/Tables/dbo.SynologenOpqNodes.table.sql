CREATE TABLE [dbo].[SynologenOpqNodes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Parent]         INT            NULL,
    [Order]          INT            NOT NULL,
    [Name]           NVARCHAR (512) NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [CreatedById]    INT            NOT NULL,
    [CreatedByName]  NVARCHAR (100) NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [ChangedById]    INT            NULL,
    [ChangedByName]  NVARCHAR (100) NULL,
    [ChangedDate]    DATETIME       NULL,
    [ApprovedById]   INT            NULL,
    [ApprovedByName] NVARCHAR (100) NULL,
    [ApprovedDate]   DATETIME       NULL,
    [LockedById]     INT            NULL,
    [LockedByName]   NVARCHAR (100) NULL,
    [LockedDate]     DATETIME       NULL
);

