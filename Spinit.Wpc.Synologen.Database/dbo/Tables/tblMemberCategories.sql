CREATE TABLE [dbo].[tblMemberCategories] (
    [cId]           INT IDENTITY (1, 1) NOT NULL,
    [cNameStringId] INT NULL,
    [cOrderId]      INT NULL,
    [cBaseGroupId]  INT NULL,
    CONSTRAINT [PK_tblMemberCategories] PRIMARY KEY CLUSTERED ([cId] ASC)
);

