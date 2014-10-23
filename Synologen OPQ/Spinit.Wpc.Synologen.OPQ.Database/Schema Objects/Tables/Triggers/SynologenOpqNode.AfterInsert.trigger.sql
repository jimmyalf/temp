-- =============================================
-- After insert nodes
-- =============================================

--ALTER TRIGGER [SynologenOpqNodes_AfterInsert] 
CREATE TRIGGER [SynologenOpqNodes_AfterInsert] 
ON [dbo].[SynologenOpqNodes]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE	@id INT,
			@parent INT,
			@order INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@id = Id,
			@parent = Parent
	FROM	INSERTED
	
	SET @order = 1
	
	IF @parent IS NULL
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqNodes
			WHERE	Parent IS NULL
		END
	ELSE
		BEGIN
			SELECT	@order = CASE WHEN MAX ([Order]) IS NOT NULL THEN MAX ([Order]) + 1 ELSE 1 END
			FROM	dbo.SynologenOpqNodes
			WHERE	Parent = @parent
		END
				
	SET @contextInfo = CAST ('DontUpdateNode' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo	
			
	UPDATE	dbo.SynologenOpqNodes 
	SET		[Order] = ISNULL (@order, 1)
	WHERE	Id = @id
	
	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END