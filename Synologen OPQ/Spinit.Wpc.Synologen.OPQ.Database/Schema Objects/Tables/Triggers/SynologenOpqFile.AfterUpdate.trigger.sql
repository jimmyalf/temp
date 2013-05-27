-- =============================================
-- After update files
-- =============================================

--ALTER TRIGGER [SynologenOpqFiles_AfterUpdate]
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

