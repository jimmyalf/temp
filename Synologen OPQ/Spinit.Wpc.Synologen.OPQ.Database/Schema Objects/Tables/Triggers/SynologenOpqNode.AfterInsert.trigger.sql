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
			@order INT
			
	SELECT	@id = Id,
			@parent = Parent
	FROM	INSERTED
	
	SET @order = 1
	
	IF @parent IS NULL
		BEGIN
			SELECT	@order = MAX ([Order]) + 1
			FROM	dbo.SynologenOpqNodes
			WHERE	Parent IS NULL
		END
	ELSE
		BEGIN
			SELECT	@order = MAX ([Order]) + 1
			FROM	dbo.SynologenOpqNodes
			WHERE	Parent = @parent
		END
		
	IF @order IS NULL
		BEGIN
			SET @order = 1
		END
			
	UPDATE	dbo.SynologenOpqNodes 
	SET		[Order] = @order
	WHERE	Id = @id
END