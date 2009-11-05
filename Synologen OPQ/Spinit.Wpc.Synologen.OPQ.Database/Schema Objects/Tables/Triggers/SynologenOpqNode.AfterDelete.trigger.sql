-- =============================================
-- After delete nodes
-- =============================================

--ALTER TRIGGER [SynologenOpqNodes_AfterDelete]
CREATE TRIGGER [SynologenOpqNodes_AfterDelete]
ON [dbo].[SynologenOpqNodes]
AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @order INT,
			@parent INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@order = [Order],
			@parent = Parent
	FROM	DELETED
	
	SET @contextInfo = CAST ('DontUpdateNode' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	

	IF @parent IS NULL
		BEGIN
			UPDATE	dbo.SynologenOpqNodes
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	Parent IS NULL
		END
	ELSE
		BEGIN
			UPDATE	dbo.SynologenOpqNodes
			SET		[Order] = [Order] - 1
			WHERE	[Order] > @order
				AND	Parent = @parent
		END	
	
	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

