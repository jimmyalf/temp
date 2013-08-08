CREATE TABLE [dbo].[SynologenOpqNodes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Parent]         INT            NULL,
    [Order]          INT            CONSTRAINT [DF_SynologenOpqNodes_Order] DEFAULT (0) NOT NULL,
    [Name]           NVARCHAR (512) NOT NULL,
    [IsMenu]         BIT            NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [CreatedById]    INT            NOT NULL,
    [CreatedByName]  NVARCHAR (100) NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [ChangedById]    INT            NULL,
    [ChangedByName]  NVARCHAR (100) NULL,
    [ChangedDate]    DATETIME       NULL,
    [ApprovedById]   INT            NULL,
    [ApprovedByName] NVARCHAR (100) NULL,
    [ApprovedDate]   DATETIME       NULL,
    [LockedById]     INT            NULL,
    [LockedByName]   NVARCHAR (100) NULL,
    [LockedDate]     DATETIME       NULL,
    CONSTRAINT [SynologenOpqNodes_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [SynologenOpqNodes_SynologenOpqNodes_FK1] FOREIGN KEY ([Parent]) REFERENCES [dbo].[SynologenOpqNodes] ([Id]),
    CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK1] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK2] FOREIGN KEY ([ChangedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK3] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[tblBaseUsers] ([cId]),
    CONSTRAINT [tblBaseUsers_SynologenOpqNodes_FK4] FOREIGN KEY ([LockedById]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);


GO
-- =============================================
-- After update nodes
-- =============================================

create TRIGGER SynologenOpqNodes_AfterUpdate
--CREATE TRIGGER [SynologenOpqNodes_AfterUpdate]
ON SynologenOpqNodes
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

GO
-- =============================================
-- After insert nodes
-- =============================================

create TRIGGER SynologenOpqNodes_AfterInsert 
--CREATE TRIGGER [SynologenOpqNodes_AfterInsert] 
ON SynologenOpqNodes
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

GO
-- =============================================
-- After delete nodes
-- =============================================

create TRIGGER SynologenOpqNodes_AfterDelete
--CREATE TRIGGER [SynologenOpqNodes_AfterDelete]
ON SynologenOpqNodes
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
