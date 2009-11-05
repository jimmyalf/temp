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
			@contextInfo VARBINARY (128)
			
	SELECT	@order = [Order],
			@ndeId = NdeId
	FROM	DELETED
	
	SET @contextInfo = CAST ('DontUpdateFile' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	

	UPDATE	dbo.SynologenOpqFiles
	SET		[Order] = [Order] - 1
	WHERE	[Order] > @order
		AND	NdeId = @ndeId
	
	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

