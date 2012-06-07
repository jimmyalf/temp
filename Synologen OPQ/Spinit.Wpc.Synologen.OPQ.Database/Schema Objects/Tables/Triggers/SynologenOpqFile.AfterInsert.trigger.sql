-- =============================================
-- After insert files
-- =============================================

--ALTER TRIGGER [SynologenOpqFiles_AfterInsert] 
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