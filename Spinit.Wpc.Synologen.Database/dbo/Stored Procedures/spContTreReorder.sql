create PROCEDURE spContTreReorder
					@parent INT,
					@orderNo INT OUTPUT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @reOrdId INT,
			@reOrdTreTpeId INT,
			@reOrdOrderLast INT
			
	SET @reOrdOrderLast = 0
	
	DECLARE get_def CURSOR LOCAL FOR
		SELECT	cId
		FROM	tblContTree
		WHERE	cParent = @parent
			AND	cTreTpeId = 5
		ORDER BY cOrder ASC
		
	OPEN get_def
	FETCH NEXT FROM get_def INTO @reOrdId
	
	WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			IF (@reOrdOrderLast = 0)
				BEGIN
					UPDATE	tblContTree
					SET		cOrder = 1
					WHERE	cId = @reOrdId
					
					SET @reOrdOrderLast = 1
				END
			ELSE
				BEGIN					
					UPDATE	tblContTree
					SET		cTreTpeId = 4
					WHERE	cId = @reOrdId
				END
		
			FETCH NEXT FROM get_def INTO @reOrdId
		END
		
	CLOSE get_def
	DEALLOCATE get_def
			
	DECLARE reorder CURSOR LOCAL FOR
		SELECT	cId,
				cTreTpeId
		FROM	tblContTree
		WHERE	cParent = @parent
			AND	cTreTpeId <> 5
		ORDER BY cOrder ASC		
		
	OPEN reorder 
	FETCH NEXT FROM reorder INTO	@reOrdId,
									@reOrdTreTpeId
									
	WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			IF ((@reOrdTreTpeId = 9)
				OR (@reOrdTreTpeId = 10)
				OR (@reOrdTreTpeId = 17))
				BEGIN
					FETCH NEXT FROM reorder INTO	@reOrdId,
													@reOrdTreTpeId
					CONTINUE
				END
		
			SET @reOrdOrderLast = @reOrdOrderLast + 1

			UPDATE	tblContTree
			SET		cOrder = @reOrdOrderLast
			WHERE	cId = @reOrdId
							
			FETCH NEXT FROM reorder INTO	@reOrdId,
											@reOrdTreTpeId
		END
		
	CLOSE reorder
	DEALLOCATE reorder
											
	SET @orderNo = @reOrdOrderLast
	
	SET @status = @@ERROR
END
