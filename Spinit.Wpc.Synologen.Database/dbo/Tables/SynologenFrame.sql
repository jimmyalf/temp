CREATE TABLE [dbo].[SynologenFrame] (
    [Id]                         INT            IDENTITY (1, 1) NOT NULL,
    [Name]                       NVARCHAR (255) NOT NULL,
    [ArticleNumber]              NVARCHAR (100) CONSTRAINT [DF_SynologenFrame_ArticleNumber] DEFAULT ((0)) NOT NULL,
    [BrandId]                    INT            NOT NULL,
    [ColorId]                    INT            NOT NULL,
    [PupillaryDistanceMin]       DECIMAL (5, 2) CONSTRAINT [DF_SynologenFrame_PupillaryDistanceMin] DEFAULT (0) NOT NULL,
    [PupillaryDistanceMax]       DECIMAL (5, 2) CONSTRAINT [DF_SynologenFrame_PupillaryDistanceMax] DEFAULT (0) NOT NULL,
    [PupillaryDistanceIncrement] DECIMAL (5, 2) CONSTRAINT [DF_SynologenFrame_PupillaryDistanceIncrement] DEFAULT (0) NOT NULL,
    [StockAtStockDate]           INT            NULL,
    [StockDate]                  DATETIME       NULL,
    [AllowOrders]                BIT            CONSTRAINT [DF_SynologenFrame_AllowOrders] DEFAULT (1) NOT NULL,
    CONSTRAINT [PK_SynologenFrame] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenFrame_SynologenFrameBrand] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[SynologenFrameBrand] ([Id]),
    CONSTRAINT [FK_SynologenFrame_SynologenFrameColor] FOREIGN KEY ([ColorId]) REFERENCES [dbo].[SynologenFrameColor] ([Id])
);

