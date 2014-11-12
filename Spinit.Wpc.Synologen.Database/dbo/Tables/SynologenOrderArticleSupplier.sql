CREATE TABLE [dbo].[SynologenOrderArticleSupplier] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    [ShippingOptions]   INT           NULL,
    [OrderEmailAddress] NVARCHAR (50) NOT NULL,
    [Active]            BIT           NOT NULL,
    CONSTRAINT [PK_SynologenOrderArticleSupplier] PRIMARY KEY CLUSTERED ([Id] ASC)
);

