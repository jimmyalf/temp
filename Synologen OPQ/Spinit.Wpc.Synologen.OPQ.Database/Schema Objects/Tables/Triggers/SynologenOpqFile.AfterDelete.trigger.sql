-- =============================================
-- After delete files
-- =============================================

--ALTER TRIGGER [SynologenOpqFiles_AfterDelete]
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

