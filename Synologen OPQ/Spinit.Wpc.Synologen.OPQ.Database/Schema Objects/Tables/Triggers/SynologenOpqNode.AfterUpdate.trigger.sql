-- =============================================
-- After update nodes
-- =============================================

CREATE TRIGGER [SynologenOpqNodes_AfterUpdate]
ON [dbo].[SynologenOpqNodes]
AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @oldOrder INT,
			@newOrder INT,
			@oldParent INT,
			@newParent INT
			
	SELECT	@oldOrder = [Order],
			@oldParent = Parent
	FROM	DELETED
	
	SELECT	@newOrder = [Order],
			@newParent = Parent
	FROM	INSERTED		

	IF @oldParent IS NULL
		BEGIN
			IF @newParent IS NULL
				BEGIN
					IF @oldOrder <> @newOrder
						BEGIN						
							IF @oldOrder > @newOrder		-- Move down
								BEGIN	
									UPDATE	dbo.SynologenOpqNodes
									SET		[Order] = [Order] + 1
									WHERE	[Order] >= @newOrder 
										AND [Order] <= @oldOrder
										AND Parent IS NULL
								END
							ELSE							-- Move up
								BEGIN
									UPDATE	dbo.SynologenOpqNodes
									SET		[Order] = [Order] - 1
									WHERE	[Order] <= @newOrder 
										AND [Order] >= @oldOrder
										AND Parent IS NULL
								END
						END
				END
			ELSE
				BEGIN
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] - 1
					WHERE	[Order] > @oldOrder
						AND	Parent IS NULL
						
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] + 1
					WHERE	[Order] >= @newOrder
						AND	Parent = @newParent
				END
		END
	ELSE 
		BEGIN
			IF @newParent IS NULL
				BEGIN
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] - 1
					WHERE	[Order] > @oldOrder
						AND	Parent = @oldParent
						
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] + 1
					WHERE	[Order] >= @newOrder
						AND	Parent IS NULL
				END
			ELSE 
				BEGIN
					IF @oldParent <> @newParent
						BEGIN
							UPDATE	dbo.SynologenOpqNodes
							SET		[Order] = [Order] - 1
							WHERE	[Order] > @oldOrder
								AND	Parent = @oldParent
								
							UPDATE	dbo.SynologenOpqNodes
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder
								AND	Parent = @newParent
						END
					ELSE
						BEGIN
							IF @oldOrder <> @newOrder
								BEGIN						
									IF @oldOrder > @newOrder		-- Move down
										BEGIN	
											UPDATE	dbo.SynologenOpqNodes
											SET		[Order] = [Order] + 1
											WHERE	[Order] >= @newOrder 
												AND [Order] <= @oldOrder
												AND Parent IS NULL
										END
									ELSE							-- Move up
										BEGIN
											UPDATE	dbo.SynologenOpqNodes
											SET		[Order] = [Order] - 1
											WHERE	[Order] <= @newOrder 
												AND [Order] >= @oldOrder
												AND Parent IS NULL
										END
								END
						END
				END
		END
		
END

