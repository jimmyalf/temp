CREATE TABLE [dbo].[SynologenOpqFiles] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Order]          INT            CONSTRAINT [DF_SynologenOpqFiles_Order] DEFAULT ((0)) NOT NULL,
    [FleCatId]       INT            NOT NULL,
    [FleId]          INT            NOT NULL,
    [NdeId]          INT            NOT NULL,
    [ShpId]          INT            NULL,
    [CncId]          INT            NULL,
    [ShopGroupId]    INT            NULL,
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
    [LockedDate]     DATETIME       NULL,
    CONSTRAINT [SynologenOpqFiles_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SynologenOpqFiles_tblSynologenShopGroup] FOREIGN KEY ([ShopGroupId]) REFERENCES [dbo].[tblSynologenShopGroup] ([Id]),
    CONSTRAINT [SynologenOpqFileCategories_SynologenOpqFiles_FK1] FOREIGN KEY ([FleCatId]) REFERENCES [dbo].[SynologenOpqFileCategories] ([Id]),
    CONSTRAINT [SynologenOpqNodes_SynologenOpqFiles_FK1] FOREIGN KEY ([NdeId]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]),
    CONSTRAINT [tblBaseFile_SynologenOpqFiles_FK1] FOREIGN KEY ([FleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK3] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqFiles_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblSynologenConcern_SynologenOpqFiles_FK1] FOREIGN KEY ([CncId]) REFERENCES [dbo].[tblSynologenConcern] ([cId]),
    CONSTRAINT [tblSynologenShop_SynologenOpqFiles_FK1] FOREIGN KEY ([ShpId]) REFERENCES [dbo].[tblSynologenShop] ([cId])
);




GO
-- =============================================
-- After delete files
-- =============================================

CREATE TRIGGER SynologenOpqFiles_AfterDelete
--CREATE TRIGGER [SynologenOpqFiles_AfterDelete]
ON dbo.SynologenOpqFiles
AFTER DELETE 
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @order INT,
			@ndeId INT,
			@shpId INT,
			@cncId INT,
			@shopGroupId INT,
			@fleCatId INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@order = [Order],
			@ndeId = NdeId,
			@shpId = ShpId,
			@cncId = CncId,
			@shopGroupId = ShopGroupId,
			@fleCatId = FleCatId
	FROM	DELETED
	
	SET @contextInfo = CAST ('DontUpdateFile' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	

	-- Treat all files  connected to both shop and shop-group as only shop-group.
	IF (@shpId IS NOT NULL) AND (@shopGroupId IS NOT NULL)
		BEGIN
			SET @shpId = NULL
		END

	IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId IS NULL
				AND CncId IS NULL
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END
	
	IF (@shpId IS NOT NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId = @shpId
				AND CncId IS NULL
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END

	IF (@shpId IS NULL) AND (@cncId IS NOT NULL) AND (@shopGroupId IS NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId IS NULL
				AND CncId = @cncId
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END

	IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NOT NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId IS NULL
				AND CncId IS NULL
				AND ShopGroupId = @shopGroupId
				AND FleCatId = @fleCatId
		END
	
	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

GO
-- =============================================
-- After update files
-- =============================================

CREATE TRIGGER SynologenOpqFiles_AfterUpdate
--CREATE TRIGGER [SynologenOpqFiles_AfterUpdate]
ON dbo.SynologenOpqFiles
AFTER UPDATE 
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @id INT,
			@oldOrder INT,
			@newOrder INT,
			@ndeId INT,
			@shpId INT,
			@cncId INT,
			@shopGroupId INT,
			@fleCatId INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@contextInfo = CONTEXT_INFO
	FROM	master.dbo.SYSPROCESSES 
	WHERE	SPID = @@SPID 
	
	IF CAST (@contextInfo AS VARCHAR (128)) = 'DontUpdateFile'
		BEGIN
			RETURN
		END
		   
	SET @contextInfo = CAST ('DontUpdateFile' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo
		
	SELECT	@id = Id,
			@oldOrder = [Order]
	FROM	DELETED
	
	SELECT	@newOrder = [Order],
			@ndeId = NdeId,
			@shpId = ShpId,
			@cncId = CncId,
			@shopGroupId = ShopGroupId,
			@fleCatId = FleCatId
	FROM	INSERTED		

	-- Treat all files  connected to both shop and shop-group as only shop-group.
	IF (@shpId IS NOT NULL) AND (@shopGroupId IS NOT NULL)
		BEGIN
			SET @shpId = NULL
		END

	IF @oldOrder <> @newOrder
		BEGIN						
			IF @oldOrder > @newOrder		-- Move down
				BEGIN
					IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder 
								AND [Order] <= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId IS NULL
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NOT NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder 
								AND [Order] <= @oldOrder
								AND NdeId = @ndeId
								AND ShpId = @shpId
								AND CncId IS NULL
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NULL) AND (@cncId IS NOT NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder 
								AND [Order] <= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId = @cncId
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NOT NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder 
								AND [Order] <= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId IS NULL
								AND ShopGroupId = @shopGroupId
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
				END
			ELSE							-- Move up
				BEGIN
					IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] - 1
							WHERE	[Order] <= @newOrder 
								AND [Order] >= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId IS NULL
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NOT NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] - 1
							WHERE	[Order] <= @newOrder 
								AND [Order] >= @oldOrder
								AND NdeId = @ndeId
								AND ShpId = @shpId
								AND CncId IS NULL
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NULL) AND (@cncId IS NOT NULL) AND (@shopGroupId IS NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] - 1
							WHERE	[Order] <= @newOrder 
								AND [Order] >= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId = @cncId
								AND ShopGroupId IS NULL
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
					
					IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NOT NULL)
						BEGIN
							UPDATE	dbo.SynologenOpqFiles
							SET		[Order] = [Order] - 1
							WHERE	[Order] <= @newOrder 
								AND [Order] >= @oldOrder
								AND NdeId = @ndeId
								AND ShpId IS NULL
								AND CncId IS NULL
								AND ShopGroupId = @shopGroupId
								AND FleCatId = @fleCatId
								AND Id <> @id
						END
				END
		END

	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

GO
-- =============================================
-- After insert files
-- =============================================

CREATE TRIGGER SynologenOpqFiles_AfterInsert 
--CREATE TRIGGER [SynologenOpqFiles_AfterInsert] 
ON dbo.SynologenOpqFiles
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE	@id INT,
			@ndeId INT,
			@shpId INT,
			@cncId INT,
			@shopGroupId INT,
			@fleCatId INT,
			@order INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@id = Id,
			@ndeId = NdeId,
			@shpId = ShpId,
			@cncId = CncId,
			@shopGroupId = ShopGroupId,
			@fleCatId = FleCatId
	FROM	INSERTED
	
	SET @order = 1
	
	-- Treat all files  connected to both shop and shop-group as only shop-group.
	IF (@shpId IS NOT NULL) AND (@shopGroupId IS NOT NULL)
		BEGIN
			SET @shpId = NULL
		END

	IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqFiles
			WHERE	NdeId = @ndeId
				AND	ShpId IS NULL
				AND	CncId IS NULL
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END
			
	IF (@shpId IS NOT NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NULL)
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqFiles
			WHERE	NdeId = @ndeId
				AND	ShpId = @shpId
				AND	CncId IS NULL
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END

	IF (@shpId IS NULL) AND (@cncId IS NOT NULL) AND (@shopGroupId IS NULL)
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqFiles
			WHERE	NdeId = @ndeId
				AND	ShpId IS NULL
				AND	CncId = @cncId
				AND ShopGroupId IS NULL
				AND FleCatId = @fleCatId
		END

	IF (@shpId IS NULL) AND (@cncId IS NULL) AND (@shopGroupId IS NOT NULL)
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqFiles
			WHERE	NdeId = @ndeId
				AND	ShpId IS NULL
				AND	CncId IS NULL
				AND ShopGroupId = @shopGroupId
				AND FleCatId = @fleCatId
		END

	SET @contextInfo = CAST ('DontUpdateFile' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	

	UPDATE	dbo.SynologenOpqFiles
	SET		[Order] = @order
	WHERE	Id = @id
	
	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END
