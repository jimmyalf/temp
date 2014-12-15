CREATE PROCEDURE spContTreCopyNode
					@copyId INT,
					@destId INT,
					@user NVARCHAR (100),
					@newId INT OUTPUT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE	@dummy INT,
			@order INT,
			@treTpeId INT,
			@parent INT,
			@destTreTpeId INT,
			@destParent INT,
			@name NVARCHAR (255),
			@nameTemp NVARCHAR (255),
			@fileName NVARCHAR (255),
			@fileNameTemp NVARCHAR (255),
			@nameCount INT,
			@fileNameCount INT,
			@nameLong INT,
			@chkNameParent INT
			
	DECLARE get_source CURSOR LOCAL FOR 
		SELECT	cTreTpeId,
				cParent,
				cName,
				cFileName
		FROM	tblContTree
		WHERE	cId = @copyId
	
	OPEN get_source
	FETCH NEXT FROM get_source INTO	@treTpeId,
									@parent,
									@name, 
									@fileName
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_source
			DEALLOCATE get_source
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_source
	DEALLOCATE get_source
		
	IF ((@treTpeId = 1)
		OR (@treTpeId = 9)
		OR (@treTpeId = 10)
		OR (@treTpeId = 17))
		BEGIN
			SET @status = -2
			
			RETURN
		END
	DECLARE get_destination CURSOR LOCAL FOR 
		SELECT	cTreTpeId,
				cParent
		FROM	tblContTree
		WHERE	cId = @destId
	
	OPEN get_destination
	FETCH NEXT FROM get_destination INTO	@destTreTpeId, @destParent

	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_destination
			DEALLOCATE get_destination
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_destination
	DEALLOCATE get_destination

	IF(@destParent = @copyId) BEGIN
			SELECT @status = -2 
			RETURN
	END
	
	-- Examine if we are about to copy a language. 
	-- If so we just copy the childnodes recursive and not the language node itself
	-- Start language copy
	
	DECLARE @childId INT
	DECLARE @child_childId INT
	DECLARE @childTreTpeId INT
	DECLARE @languageDestId INT
	IF (@treTpeId = 2)
	BEGIN
		-- We only allow copy from language to language
		IF (@destTreTpeId <> 2)
		BEGIN
			SET @status = -2			
			RETURN
		END		
		DECLARE getChildNodes CURSOR LOCAL FOR
			SELECT cId, cTreTpeId FROM tblContTree
			WHERE cParent = @copyId
		OPEN getChildNodes
		FETCH NEXT FROM getChildNodes INTO @childId, @childTreTpeId
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			-- Now we need to examine the type of the node.
			-- If we have a trashcan, archive or include we don't wan't the node itself to be copied. 
			-- Just the childs of that node
			IF ((@childTreTpeId = 9) OR (@childTreTpeId = 10) OR (@childTreTpeId = 17))
			BEGIN
				-- CopyNode has a child as Archive/Trash/IncludeMenu. Will now attempt to dig up its children.
				-- Now we need to find the corresponding type in the destination node
				SET @languageDestId = -1
				SELECT TOP 1 @languageDestId = cId 
				FROM tblContTree 
				WHERE cParent = @destId AND cTreTpeId = @childTreTpeId
				IF (@@ROWCOUNT > 0) BEGIN
					DECLARE getChild_ChildNodes CURSOR LOCAL FOR
					SELECT cId FROM tblContTree
					WHERE cParent = @childId
					OPEN getChild_ChildNodes
					FETCH NEXT FROM getChild_ChildNodes INTO @child_childId
					WHILE (@@FETCH_STATUS <> -1) BEGIN
						-- Recursively calling spContTreCopyNode (CopyNode has a child as Archive/Trash/IncludeMenu).' 
						EXECUTE spContTreCopyNode	@child_childId, 
													@languageDestId,
													@user,
													@newId OUTPUT,
													@newId OUTPUT		
						FETCH NEXT FROM getChild_ChildNodes INTO @child_childId							
					END
					CLOSE getChild_ChildNodes
					DEALLOCATE getChild_ChildNodes					
				END
			END
			ELSE BEGIN -- Normal Page or Menu so we just need to copy
				EXECUTE spContTreCopyNode	@childId, 
											@destId,
											@user,
											@newId OUTPUT,
											@newId OUTPUT
			END
			FETCH NEXT FROM getChildNodes INTO @childId, @childTreTpeId
		END
		CLOSE getChildNodes
		DEALLOCATE getChildNodes
		-- Set the new id to the destination Id witch in this case is the language
		SET @newId = @destId
		SET @status = @@ERROR --//CBER
		RETURN
	END
	
	-- End language copy
/*
	IF ((@destTreTpeId = 16) OR (@destTreTpeId = 17))
		BEGIN
			PRINT 'spContTreCopyNode: Exit cause destNode is IncludePage/IncludeMenu.' 
			SET @status = -2
			
			RETURN
		END
*/		
	-- Exit cause copyNode is IncludePage and destNode isnt IncludeMenu.
	IF ((@treTpeId = 16)
		AND (@destTreTpeId <> 17))
		BEGIN			
			SET @status = -2
			
			RETURN
		END
		
	-- Exit because destNode is Location and copyNode isnt Language.
	IF ((@destTreTpeId = 1)
		AND (@treTpeId <> 2))
		BEGIN
			SET @status = -2
			
			RETURN
		END
	ELSE
		 BEGIN	
			-- Exit because copyNode isnt Menu/Page/DefaultPage/LinkPage/IncludePage.
			IF ((@treTpeId <> 3)
						AND (@treTpeId <> 4)
						AND (@treTpeId <> 5)
						AND (@treTpeId <> 12)
						AND (@treTpeId <> 16))
				BEGIN
					SET @status = -2
					RETURN
				END
		END
		
	IF ((@destTreTpeId = 2) AND ((@treTpeId = 4) OR (@treTpeId = 5)))
		BEGIN
			-- Dest=Language. copyNode=Page/DefaultPage. 
			DECLARE chk_def CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContTree
				WHERE	cId = @destId
					AND	cTreTpeId = 5
					
			OPEN chk_def
			FETCH NEXT FROM chk_def INTO @dummy
			
			IF (@@FETCH_STATUS <> -1) BEGIN
					CLOSE chk_def
					DEALLOCATE chk_def
					
					SET @status = -2
					RETURN
			END
				
			CLOSE chk_def
			DEALLOCATE chk_def
			
			-- copyNode = Page. changing to DefaultPage.
			IF (@treTpeId = 4)
				BEGIN
					SET @treTpeId = 5
				END
		END
		--Name length
		IF (LEN (@name) > 35)
			BEGIN					
				SELECT @name = LEFT (@name, 30)
				SELECT @nameTemp = LEFT (@name, 30)
				SELECT @nameLong = 1
			END
		ELSE
			BEGIN
				SELECT @nameTemp = @name
				SELECT @nameLong = 0
			END

	-- destNode=Page/DefaultPage/LinkPage OR (copyNode=Menu AND destNode =Menu)
	IF ((@destTreTpeId = 4) 
		OR (@destTreTpeId = 5) 
		OR (@destTreTpeId = 12)
		OR ((@treTpeId = 3) AND (@destTreTpeId = 3)))
		BEGIN 
			SET @chkNameParent = @destParent
		END
	ELSE
		BEGIN
			SET @chkNameParent = @destId
		END
		
		--CHECK NAME
		DECLARE chk_nameExist CURSOR LOCAL FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cName = @nameTemp
				AND cParent = @chkNameParent

		OPEN chk_nameExist
		FETCH NEXT FROM chk_nameExist INTO @dummy
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist

				SELECT @nameCount = 2
					
				WHILE (@nameCount > 0)
					BEGIN
						SELECT @nameTemp = 'Copy of (' 
								+ CAST (@nameCount AS NVARCHAR (3))
								+ ') ' + @name
						IF (@nameLong = 1)
							BEGIN
								SELECT @nameTemp = @nameTemp + '...'
							END
						
						DECLARE chk_nameExist2 CURSOR LOCAL FOR
							SELECT	1
							FROM	tblContTree
							WHERE	cName = @nameTemp
								AND cParent = @chkNameParent
						
						OPEN chk_nameExist2
						FETCH NEXT FROM chk_nameExist2 INTO @dummy
						
						IF (@@FETCH_STATUS <> -1)
							BEGIN
								SELECT @nameCount = @nameCount + 1
							END
						ELSE
							BEGIN
								SELECT @nameCount = 0
							END
							
						CLOSE chk_nameExist2
						DEALLOCATE chk_nameExist2
					END
			END
		ELSE
			BEGIN
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist
			END

-----------------------------------------------------------------------
		--Check FILE-NAME
		PRINT 'Starting file name change'
		SELECT @fileNameTemp = SUBSTRING(@fileName,0, 255)

		DECLARE chk_fileNameExist CURSOR LOCAL FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cFileName LIKE @fileNameTemp 
				AND cParent = @chkNameParent

		OPEN chk_fileNameExist
		FETCH NEXT FROM chk_fileNameExist INTO @dummy
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chk_fileNameExist
				DEALLOCATE chk_fileNameExist

				SELECT @fileNameCount = 2
					
				WHILE (@fileNameCount > 0)
					BEGIN
						SELECT @fileNameTemp = 'Copy of (' 
								+ CAST (@fileNameCount AS NVARCHAR (3))
								+ ') ' + @fileName
						
						DECLARE chk_nameExist2 CURSOR LOCAL FOR
							SELECT	1
							FROM	tblContTree
							WHERE	cFileName LIKE @fileNameTemp 
								AND cParent = @chkNameParent
						
						OPEN chk_nameExist2
						FETCH NEXT FROM chk_nameExist2 INTO @dummy
						
						IF (@@FETCH_STATUS <> -1)
							BEGIN
								SELECT @fileNameCount = @fileNameCount + 1
							END
						ELSE
							BEGIN
								SELECT @fileNameCount = 0
							END
							
						CLOSE chk_nameExist2
						DEALLOCATE chk_nameExist2
					END
			END
		ELSE
			BEGIN
				CLOSE chk_fileNameExist
				DEALLOCATE chk_fileNameExist
			END
		PRINT 'Done with file name change'
-----------------------------------------------------------------------


	IF ((@destTreTpeId = 4) 
		OR (@destTreTpeId = 5) 
		OR (@destTreTpeId = 12)
		OR ((@treTpeId = 3) AND (@destTreTpeId = 3)))
		BEGIN
			PRINT 'spContTreCopyNode: destNode=Page/DefaultPage/LinkPage OR (copyNode=Menu AND destNode =Menu)'
			PRINT 'spContTreCopyNode: Execute spContTreReorder(dest='+CAST(@destParent AS VARCHAR(30))+').'
			EXECUTE spContTreReorder	@destParent, 
										@order OUTPUT, 
										@status OUTPUT
										
			DECLARE get_dest_order CURSOR LOCAL FOR
				SELECT	cOrder
				FROM	tblContTree
				WHERE	cId = @destId
				
			OPEN get_dest_order
			FETCH NEXT FROM get_dest_order INTO	@order
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					PRINT 'spContTreCopyNode: Abort with status -1. DestOrder cannot be fetched.'
					CLOSE get_dest_order
					DEALLOCATE get_dest_order
					
					SET @status = -1
					RETURN
				END
				
			CLOSE get_dest_order
			DEALLOCATE get_dest_order
			
			PRINT 'spContTreCopyNode: Increasing node-order (destOrder +1).'
			SET @order = @order + 1
			PRINT 'spContTreCopyNode: Calling spContTreCopyNodeRec with destParent as dest.'
			EXECUTE spContTreCopyNodeRec	@copyId,
											@destParent,
											@order,
											@user,
											@nameTemp,
											@fileNameTemp,
											@newId OUTPUT,
											@status OUTPUT			
		END
	ELSE
		BEGIN
			PRINT 'spContTreCopyNode: destNode=Page/DefaultPage/LinkPage OR (copyNode=Menu AND destNode =Menu) -> Else'
			PRINT 'spContTreCopyNode: Calling spContTreReorder.'
			EXECUTE spContTreReorder	@destId, 
										@order OUTPUT, 
										@status OUTPUT
			PRINT 'spContTreCopyNode: Calling spContTreCopyNodeRec.'
			EXECUTE spContTreCopyNodeRec	@copyId,
											@destId,
											@order,
											@user,
											@nameTemp,
											@fileNameTemp,
											@newId OUTPUT,
											@status OUTPUT
		END			
END
