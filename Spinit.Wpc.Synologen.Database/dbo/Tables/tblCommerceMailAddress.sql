CREATE TABLE [dbo].[tblCommerceMailAddress] (
    [cId]          INT            IDENTITY (1, 1) NOT NULL,
    [cMlId]        INT            NOT NULL,
    [cEmail]       NVARCHAR (512) NOT NULL,
    [cIsActive]    BIT            NOT NULL,
    [cCreatedBy]   NVARCHAR (100) NOT NULL,
    [cCreatedDate] DATETIME       NOT NULL,
    [cChangedBy]   NVARCHAR (100) NULL,
    [cChangedDate] DATETIME       NULL,
    CONSTRAINT [PK_tblCommerceMailAddress] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCommerceMailAddress_tblQuickMailMail] FOREIGN KEY ([cMlId]) REFERENCES [dbo].[tblQuickMailMail] ([cId])
);

