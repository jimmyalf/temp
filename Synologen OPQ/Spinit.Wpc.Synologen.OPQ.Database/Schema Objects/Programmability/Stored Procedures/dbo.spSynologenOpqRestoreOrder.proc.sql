--ALTER PROCEDURE [dbo].[spSynologenOpqRestoreOrder]
CREATE PROCEDURE [dbo].[spSynologenOpqRestoreOrder]
	@parent int
AS
BEGIN
	DECLARE @id INT,
			@order INT
			
	SET @order = 1
	
	IF @parent IS NULL
		BEGIN
			DECLARE GetChilds CURSOR LOCAL FOR
				SELECT	Id
				FROM	dbo.SynologenOpqNodes
				WHERE	Parent IS NULL
				ORDER BY [Order] ASC
				
			OPEN GetChilds
			FETCH NEXT FROM GetChilds INTO @id
			
			WHILE @@FETCH_STATUS <> -1
				BEGIN
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = @order
					WHERE	Id = @id
					
					SET @order = @order + 1
					
					FETCH NEXT FROM GetChilds INTO @id
				END
			
			CLOSE GetChilds
			DEALLOCATE GetChilds
		END
	ELSE
		BEGIN
			DECLARE GetChilds CURSOR LOCAL FOR
				SELECT	Id
				FROM	dbo.SynologenOpqNodes
				WHERE	Parent = @parent
				ORDER BY [Order] ASC
				
			OPEN GetChilds
			FETCH NEXT FROM GetChilds INTO @id
			
			WHILE @@FETCH_STATUS <> -1
				BEGIN
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = @order
					WHERE	Id = @id
					
					SET @order = @order + 1
					
					FETCH NEXT FROM GetChilds INTO @id
				END
			
			CLOSE GetChilds
			DEALLOCATE GetChilds
		END
	
END