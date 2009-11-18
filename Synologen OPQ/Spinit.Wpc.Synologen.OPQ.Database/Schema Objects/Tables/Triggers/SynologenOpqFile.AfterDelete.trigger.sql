-- =============================================
-- After delete files
-- =============================================

--ALTER TRIGGER [SynologenOpqFiles_AfterDelete]
CREATE TRIGGER [SynologenOpqFiles_AfterDelete]
ON [dbo].[SynologenOpqFiles]
AFTER DELETE 
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @order INT,
			@ndeId INT,
			@shpId INT,
			@cncId INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@order = [Order],
			@ndeId = NdeId,
			@shpId = ShpId,
			@cncId = CncId
	FROM	DELETED
	
	SET @contextInfo = CAST ('DontUpdateFile' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	

	IF (@shpId IS NULL) AND (@cncId IS NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId IS NULL
				AND CncId IS NULL
		END
	
	IF (@shpId IS NOT NULL) AND (@cncId IS NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId = @shpId
				AND CncId IS NULL
		END

	IF (@shpId IS NULL) AND (@cncId IS NOT NULL)
		BEGIN
			UPDATE	dbo.SynologenOpqFiles
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	NdeId = @ndeId
				AND ShpId IS NULL
				AND CncId = @cncId
		END

	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

