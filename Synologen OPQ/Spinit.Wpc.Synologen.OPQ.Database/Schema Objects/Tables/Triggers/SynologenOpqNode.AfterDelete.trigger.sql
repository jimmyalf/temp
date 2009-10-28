-- =============================================
-- After delete nodes
-- =============================================

CREATE TRIGGER [SynologenOpqNodes_AfterDelete]
ON [dbo].[SynologenOpqNodes]
AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @order INT,
			@parent INT
			
	SELECT	@order = [Order],
			@parent = Parent
	FROM	DELETED
	
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
END

