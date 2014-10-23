CREATE PROCEDURE spDocumentAddUpdateDeleteDocument
	@action INT,
	@id INT OUTPUT,                
	@nodeId INT = 0,
	@name NVARCHAR(100)= '',
	@contentInfo NVARCHAR(50)= '',
	@size INT = 0,
	@content IMAGE = NULL,
	@description NVARCHAR(255)= '',
	@createdBy NVARCHAR (50)= '',
	@changedDate smalldatetime= '',	
	@changedBy NVARCHAR (50)= '',
	@groupId INT = -1,
	@userId INT = -1,
	@status INT OUTPUT	
AS
		BEGIN TRANSACTION ADD_UPDATE_DELETE_DOCUMENT
		IF (@action = 0) -- Create
		BEGIN
			DECLARE @max INT

			/*Get max*/
			SELECT @max = MAX (cOrder)
			FROM	tblDocuments
			WHERE cNodeId = @nodeId
			AND cOrder IS NOT NULL
			IF @max IS NULL
			BEGIN
				SET @max = 0
			END

		
			INSERT INTO tblDocuments
				(cNodeId, cName, cContentInfo, cSize, cContent, cDescription, cCreatedDate, cCreatedBy, cGroupId, cUserId, cOrder)				
			VALUES
				(@nodeId, @name, @contentInfo, @size, @content, @description, GETDATE(), @createdBy, @groupId, @userId, @max + 1)				
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN
			UPDATE tblDocuments
			SET cNodeId = @nodeId, 
				cName = @name, 
				cContentInfo = @contentInfo, 
				cSize = @size, 
				--cContent = @content, 
				cDescription = @description,
				cChangedDate = GETDATE(),
				cCreatedBy = @createdBy ,
				cGroupId = @groupId,
				cUserId = @userId	
			WHERE cId = @id
		END
		IF (@action = 2) -- Delete
		BEGIN
			
			DECLARE @order INT
			DECLARE @node INT
			
			SELECT	@order = cOrder,
					@node = cNodeId
			FROM	tblDocuments
			WHERE	cId = @id
			
			-- Delete connections
			DELETE FROM dbo.tblDocumentDocumentFileConnection
			WHERE cDocumentId = @id
			
			DELETE FROM tblDocuments
			WHERE cId = @id
			
			UPDATE tblDocuments
			SET cOrder = cOrder - 1 
			WHERE cNodeId = @node
			AND cOrder >  @order
			
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_DOCUMENT
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_DOCUMENT
			END
