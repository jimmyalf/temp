CREATE TABLE [dbo].[SynologenFrameGlassType] (
    [Id]                               INT            IDENTITY (1, 1) NOT NULL,
    [Name]                             NVARCHAR (255) NOT NULL,
    [IncludeAdditionParametersInOrder] BIT            NOT NULL,
    [IncludeHeightParametersInOrder]   BIT            NOT NULL,
    [SphereMin]                        DECIMAL (5, 2) NOT NULL,
    [SphereMax]                        DECIMAL (5, 2) NOT NULL,
    [SphereIncrement]                  DECIMAL (5, 2) NOT NULL,
    [CylinderMin]                      DECIMAL (5, 2) NOT NULL,
    [CylinderMax]                      DECIMAL (5, 2) NOT NULL,
    [CylinderIncrement]                DECIMAL (5, 2) NOT NULL,
    CONSTRAINT [PK_SynologenFrameGlassType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

