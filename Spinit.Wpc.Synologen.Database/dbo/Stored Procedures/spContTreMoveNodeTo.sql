CREATE PROCEDURE spContTreMoveNodeTo
					@id INT,
					@destId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE	@dummy INT,
			@order INT,
			@treTpeId INT,
			@parent INT,
			@destTreTpeId INT,
			@destParent INT, 
			@destLoc INT, 
			@destLng INT,
			@sourceLoc INT, 
			@sourceLng INT, 
			@leafNodeId INT

	DECLARE get_source CURSOR LOCAL FOR 
		SELECT	cTreTpeId,
				cParent,
				cLocId, 
				cLngId  
		FROM	tblContTree
		WHERE	cId = @id
	
	OPEN get_source
	--Get moveNode type and parentNodeId
	FETCH NEXT FROM get_source INTO	@treTpeId, @parent, @sourceLoc, @sourceLng 
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_source
			DEALLOCATE get_source
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_source
	DEALLOCATE get_source
	
	--Don't allow move of the types below
	IF ((@treTpeId = 1) --Location
		OR (@treTpeId = 9) --Archive
		OR (@treTpeId = 10) --Trash
		OR (@treTpeId = 17)) --Includes
		BEGIN
			SET @status = -2
			RETURN
		END
		
	DECLARE get_destination CURSOR LOCAL FOR 
		SELECT	cTreTpeId,
				cParent,
				cLocId,
				cLngId 
		FROM	tblContTree
		WHERE	cId = @destId
	
	OPEN get_destination
	FETCH NEXT FROM get_destination INTO	@destTreTpeId, @destParent, @destLoc, @destLng 
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_destination
			DEALLOCATE get_destination
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_destination
	DEALLOCATE get_destination
	
	--Don't allow move to IncludePage/IncludeMenu
	IF ((@destTreTpeId = 16)
		OR (@destTreTpeId = 17))
		BEGIN
			SET @status = -2
			
			RETURN
		END
	--Don't allow move of IncludePage anywhere but to the Trash
	IF ((@treTpeId = 16) AND (@destTreTpeId <> 10))
		BEGIN
			SET @status = -2
			
			RETURN
		END
	--You cannot chose location anymore
	--Don't allow move when destination is Location and moveNode is not language	
	IF ((@destTreTpeId = 1) AND (@treTpeId <> 2))
		BEGIN
			SET @status = -2
			
			RETURN
		END
	ELSE
		 BEGIN	
			IF ((@treTpeId <> 3) --Not Menu
						AND (@treTpeId <> 4) --Not Page
						AND (@treTpeId <> 5) --Not DefaultPage
						AND (@treTpeId <> 12) --Not LinkPage
						AND (@treTpeId <> 16)) --Not IncludePage
				BEGIN
					SET @status = -2
					
					RETURN
				END
		END
	
	--If destNode=Lang AND moveNode=Page/DefaultPage
	IF ((@destTreTpeId = 2) AND ((@treTpeId = 4) OR (@treTpeId = 5)))
		BEGIN
			DECLARE chk_def CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContTree
				WHERE	cId = @destId
					AND	cTreTpeId = 5
					
			OPEN chk_def
			FETCH NEXT FROM chk_def INTO @dummy
			
			--destNode is a defaultPage
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_def
					DEALLOCATE chk_def
					
					SET @status = -2
					RETURN
				END
				
			CLOSE chk_def
			DEALLOCATE chk_def
			
			--if moveNode type is Page -> DefaultPage
			IF (@treTpeId = 4)
				BEGIN
					SET @treTpeId = 5
				END
		END
	
	IF ((@destTreTpeId = 4) --destNode=Page
		OR (@destTreTpeId = 5) --destNode=DefaultPage
		OR (@destTreTpeId = 12) --destNode=LinkPage
		OR ((@treTpeId = 3) AND (@destTreTpeId = 3))) -- (copyNode=Menu AND destNode=Menu)
		BEGIN
			--Reorder parent
			EXECUTE spContTreReorder	@destParent, @order OUTPUT, @status OUTPUT
										
			DECLARE get_dest_order CURSOR LOCAL FOR
				SELECT	cOrder
				FROM	tblContTree
				WHERE	cId = @destId
				
			OPEN get_dest_order
			FETCH NEXT FROM get_dest_order INTO	@order
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_dest_order
					DEALLOCATE get_dest_order
					
					SET @status = -1
					RETURN
				END
				
			CLOSE get_dest_order
			DEALLOCATE get_dest_order

			--Increase order of everything "under" the destination-node by one (skip Archive/Trash/IncludeMenu)
			UPDATE tblContTree
			SET		cOrder = cOrder + 1
			WHERE	cParent = @destParent
				AND cTreTpeId  NOT IN (9, 10, 17)
				AND	cOrder > @order				
			--Insert moveNode under destinationNode
			UPDATE	tblContTree
			SET		cParent = @destParent,
					cOrder = @order + 1 ,
					cLocId = @destLoc, 
					cLngId = @destLng
			WHERE	cId = @id	
			
		END
	ELSE
		BEGIN
			--Reoder parent
			EXECUTE spContTreReorder	@destId, @order OUTPUT, @status OUTPUT
			--Insert inside?
			UPDATE	tblContTree
			SET		cParent = @destId,
					cOrder = @order,
					cLocId = @destLoc, 
					cLngId = @destLng
			WHERE	cId = @id	
		END	
		--Change Loc & Lang for leaf nodes of moved node (if needed).
		if ( (@sourceLoc <> @destLoc) OR (@sourceLng <> @destLng) ) BEGIN
			DECLARE getLeaf CURSOR LOCAL FOR
			SELECT * FROM sfContGetTreeLeaves(@id)

			OPEN getLeaf
			FETCH NEXT FROM getLeaf INTO @leafNodeId
			WHILE (@@FETCH_STATUS <> -1) BEGIN

				UPDATE	tblContTree
				SET		cLocId = @destLoc, 
						cLngId = @destLng
				WHERE	cId = @leafNodeId
			
				FETCH NEXT FROM getLeaf INTO @leafNodeId
			END
			CLOSE getLeaf
			DEALLOCATE getLeaf	
		END
END
