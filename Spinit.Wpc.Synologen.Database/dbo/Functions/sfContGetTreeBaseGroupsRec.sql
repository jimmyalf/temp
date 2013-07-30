create FUNCTION sfContGetTreeBaseGroupsRec (@treId INT)
	RETURNS
		@retMenu TABLE	(cTreId INT,
						 cBseGrpid INT)
	AS
		BEGIN
			DECLARE	@parent INT,
					@bseGrpId INT
			
			DECLARE @retTable TABLE	(cTreId INT,
									 cBseGrpId INT)
									 			
			DECLARE get_treBseGrp CURSOR LOCAL FOR
				SELECT	cBseGrpId
				FROM	tblContTreeBaseGroups
				WHERE	cTreId = @treId
				
			OPEN get_treBseGrp
			FETCH NEXT FROM get_treBseGrp INTO @bseGrpId
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					WHILE (@@FETCH_STATUS <> -1)
						BEGIN
							INSERT @retTable VALUES (@treId, @bseGrpId)
								
							FETCH NEXT FROM get_treBseGrp INTO @bseGrpId
						END
					
					CLOSE get_treBseGrp
					DEALLOCATE get_treBseGrp
				END
			ELSE
				BEGIN
					CLOSE get_treBseGrp
					DEALLOCATE get_treBseGrp
					
					DECLARE get_node CURSOR LOCAL FOR 
						SELECT	cParent
						FROM	tblContTree
						WHERE	cId = @treId
						
					OPEN get_node
					FETCH NEXT FROM get_node INTO @parent
					
					IF (@@FETCH_STATUS = -1)
						BEGIN
							CLOSE get_node
							DEALLOCATE get_node	
						END
					ELSE
						BEGIN
							CLOSE get_node
							DEALLOCATE get_node
							
							IF (@parent IS NOT NULL)
								BEGIN
									INSERT @retTable
										SELECT	cTreId,
												cBseGrpId
										FROM	sfContGetTreeBaseGroupsRec (@parent)										
								END									
						END
				END
			
			INSERT @retMenu
				SELECT	cTreId,
						cBseGrpId
				FROM	@retTable
			
			RETURN
		END
