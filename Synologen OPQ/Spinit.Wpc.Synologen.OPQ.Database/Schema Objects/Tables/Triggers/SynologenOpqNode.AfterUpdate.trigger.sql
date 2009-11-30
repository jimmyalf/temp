-- =============================================
-- After update nodes
-- =============================================

--ALTER TRIGGER [SynologenOpqNodes_AfterUpdate]
CREATE TRIGGER [SynologenOpqNodes_AfterUpdate]
ON [dbo].[SynologenOpqNodes]
AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON
	
	DECLARE @id INT,
			@oldOrder INT,
			@newOrder INT,
			@oldParent INT,
			@newParent INT,
			@contextInfo VARBINARY (128)
			
	SELECT	@contextInfo = CONTEXT_INFO
	FROM	master.dbo.SYSPROCESSES 
	WHERE	SPID = @@SPID 
	
	IF CAST (@contextInfo AS VARCHAR (128)) = 'DontUpdateNode'
		BEGIN
			RETURN
		END
		   
	SET @contextInfo = CAST ('DontUpdateNode' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo
		
	SELECT	@id = Id,
			@oldOrder = [Order],
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
										AND Id <> @id
								END
							ELSE							-- Move up
								BEGIN
									UPDATE	dbo.SynologenOpqNodes
									SET		[Order] = [Order] - 1
									WHERE	[Order] <= @newOrder 
										AND [Order] >= @oldOrder
										AND Parent IS NULL
										AND Id <> @id
								END
						END
									
						EXECUTE dbo.spSynologenOpqRestoreOrder	NULL
				END
			ELSE
				BEGIN
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] - 1
					WHERE	[Order] > @oldOrder
						AND	Parent IS NULL
						AND Id <> @id
						
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] + 1
					WHERE	[Order] >= @newOrder
						AND	Parent = @newParent
						AND Id <> @id
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
						AND Id <> @id
						
					UPDATE	dbo.SynologenOpqNodes
					SET		[Order] = [Order] + 1
					WHERE	[Order] >= @newOrder
						AND	Parent IS NULL
						AND Id <> @id
				END
			ELSE 
				BEGIN
					IF @oldParent <> @newParent
						BEGIN
							UPDATE	dbo.SynologenOpqNodes
							SET		[Order] = [Order] - 1
							WHERE	[Order] > @oldOrder
								AND	Parent = @oldParent
								AND Id <> @id
								
							UPDATE	dbo.SynologenOpqNodes
							SET		[Order] = [Order] + 1
							WHERE	[Order] >= @newOrder
								AND	Parent = @newParent
								AND Id <> @id
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
												AND Parent = @newParent
												AND Id <> @id
										END
									ELSE							-- Move up
										BEGIN
											UPDATE	dbo.SynologenOpqNodes
											SET		[Order] = [Order] - 1
											WHERE	[Order] <= @newOrder 
												AND [Order] >= @oldOrder
												AND Parent = @newParent
												AND Id <> @id
										END
										
									EXECUTE dbo.spSynologenOpqRestoreOrder	@oldParent
								END
						END
				END
		END

	SET @contextInfo = CAST ('UpdateClear' + SPACE (128) AS VARBINARY (128))  
	SET CONTEXT_INFO @contextInfo		
END

