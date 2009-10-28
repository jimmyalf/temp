-- =============================================
-- Instead-of update document
-- =============================================

--ALTER TRIGGER [SynologenOpqFiles_AfterInsert] 
CREATE TRIGGER [SynologenOpqFiles_AfterInsert] 
ON [dbo].[SynologenOpqFiles]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE	@id INT,
			@ndeId INT,
			@order INT
			
	SELECT	@id = Id,
			@ndeId = NdeId
	FROM	INSERTED
	
	SET @order = 1
	
	SELECT	@order = MAX ([Order]) + 1
	FROM	dbo.SynologenOpqFiles
	WHERE	NdeId = @ndeId
			
	IF @order IS NULL
		BEGIN
			SET @order = 1
		END

	UPDATE	dbo.SynologenOpqFiles
	SET		[Order] = @order
	WHERE	Id = @id
END