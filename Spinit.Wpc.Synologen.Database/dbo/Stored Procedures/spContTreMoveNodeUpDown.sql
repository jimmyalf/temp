create PROCEDURE spContTreMoveNodeUpDown
					@up BIT,
					@id INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE	@dummy INT,
			@order INT,
			@treTpeId INT,
			@parent INT,
			@maxOrder INT,
			@minPos INT,
			@neigbourSortPosition INT,
			@neigbourNodeId INT
			
	DECLARE get_parent CURSOR LOCAL FOR 
		SELECT	cParent
		FROM	tblContTree
		WHERE	cId = @id

	OPEN get_parent
	FETCH NEXT FROM get_parent INTO	@parent
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_parent
			DEALLOCATE get_parent
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_parent
	DEALLOCATE get_parent

	EXECUTE spContTreReorder @parent, @order OUTPUT, @status OUTPUT

	DECLARE get_order CURSOR LOCAL FOR 
		SELECT	cOrder,
				cTreTpeId
		FROM	tblContTree
		WHERE	cId = @id

	OPEN get_order
	FETCH NEXT FROM get_order INTO	@order,
									@treTpeId
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_order
			DEALLOCATE get_order
			
			SELECT @status = -1
			RETURN
		END
		
	CLOSE get_order
	DEALLOCATE get_order
	
	DECLARE get_maxOrder CURSOR LOCAL FOR
		SELECT	MAX (cOrder)
		FROM	tblContTree
		WHERE	cParent =	(SELECT	cParent
							 FROM	tblContTree
							 WHERE	cId = @id)
			AND cTreTpeId <> 9
			AND cTreTpeId <> 10
			AND cTreTpeId <> 17
			
	OPEN get_maxOrder
	FETCH NEXT FROM get_maxOrder INTO @maxOrder
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_maxOrder
			DEALLOCATE get_maxOrder
			
			SELECT @status = -2
			RETURN
		END
		
	CLOSE get_maxOrder
	DEALLOCATE get_maxOrder

	IF (@up = 1)
		BEGIN
			DECLARE get_parentDef CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContTree
				WHERE	cParent = @parent
					AND	cTreTpeId = 5
					
			OPEN get_parentDef
			FETCH NEXT FROM get_parentDef INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					SELECT @minPos = 1
				END
			ELSE
				BEGIN
					SELECT @minPos = 2
				END
				
			CLOSE get_parentDef
			DEALLOCATE get_parentDef	
			
			IF ((@order = @minPos) 
				OR (@treTpeId = 5) 
				OR (@order > (2147483645 - 10)))
				BEGIN
					SELECT @status = -3
					RETURN
				END
				
			SELECT TOP 1	
					@neigbourSortPosition = cOrder,
					@neigbourNodeId = cId 
			FROM	tblContTree
			WHERE	cOrder < @order  
			AND		cParent = @parent
			ORDER BY cOrder DESC

			UPDATE	tblContTree
			SET		cOrder = @neigbourSortPosition
			WHERE	cId = @id

			UPDATE	tblContTree
			SET		cOrder = @order
			WHERE	cId = @neigbourNodeId
		END
	ELSE
		BEGIN
			SELECT	TOP 1	
					@neigbourSortPosition = cOrder,
					@neigbourNodeId = cId 
			FROM	tblContTree
			WHERE	cOrder > @order
			AND		cParent = @parent
			ORDER BY cOrder ASC

			IF ((@order = @maxOrder) 
				OR (@treTpeId = 5)
				OR (@order > (2147483645 - 10))
				OR (@neigbourSortPosition > (2147483645 - 10)))
				BEGIN
					SELECT @status = -3
					RETURN
				END
				
			UPDATE	tblContTree
			SET		cOrder = @neigbourSortPosition
			WHERE	cId = @id

			UPDATE	tblContTree
			SET		cOrder = @order
			WHERE	cId = @neigbourNodeId
		END	
END
