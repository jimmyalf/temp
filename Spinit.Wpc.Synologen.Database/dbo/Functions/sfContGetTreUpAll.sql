create FUNCTION sfContGetTreUpAll (@id INT)
	RETURNS
		@retTree TABLE (cId	INT,
						cTreTpeId INT,
						cParent INT,
						cName NVARCHAR (500),
						cFileName NVARCHAR (500),
						cLocId INT,
						cLngId INT,
						cHideInMenu INT)
	AS
		BEGIN
			DECLARE	@searchId INT,
					@treId INT,
					@parent INT,
					@loop INT
					
			DECLARE	@retTable TABLE (cId INT,
									 cTreTpeId INT,
									 cParent INT,
									 cName NVARCHAR (500),
									 cFileName NVARCHAR (500),
									 cLocId INT,
									 cLngId INT,
									 cHideInMenu INT)
									 
			SELECT @searchId = @id
			
			DECLARE get_node CURSOR FOR
				SELECT	cParent
				FROM	tblContTree
				WHERE	cId = @id
				
							   
			OPEN get_node
			FETCH NEXT FROM get_node INTO	@parent
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					SELECT @loop = 0
				END
			ELSE
				BEGIN
					BEGIN
						IF (@parent IS NULL)
							BEGIN
								SELECT @loop = 0
								INSERT @retTable
									SELECT	cId,
											cTreTpeId,
											cParent,
											cName,
											cFileName,
											cLocId,
											cLngId,
											cHideInMenu
									FROM	tblContTree
									WHERE	cId = @id
							END
						ELSE
							BEGIN
								SELECT @loop = 1
								INSERT @retTable
									SELECT	cId,
											cTreTpeId,
											cParent,
											cName,
											cFileName,
											cLocId,
											cLngId,
											cHideInMenu
									FROM	tblContTree
									WHERE	cId = @id
							END
					END
				END
				
			CLOSE get_node
			DEALLOCATE get_node
							
			WHILE (@loop = 1)
				BEGIN
					DECLARE get_parent CURSOR FOR
						SELECT	cId,
								cParent
						FROM	tblContTree
						WHERE	cId = (SELECT	cParent
									   FROM		tblContTree
									   WHERE	cId = @searchId)
									   									   
					OPEN get_parent
					FETCH NEXT FROM get_parent INTO	@treId,
													@parent
					
					IF (@@FETCH_STATUS = -1)
						BEGIN
							SELECT @loop = 0
						END
					ELSE
						BEGIN
							IF (@parent IS NULL)
								BEGIN
									SELECT @loop = 0
									INSERT @retTable
										SELECT	cId,
												cTreTpeId,
												cParent,
												cName,
												cFileName,
												cLocId,
												cLngId,
												cHideInMenu
										FROM	tblContTree
										WHERE	cId = @treId
								END
							ELSE
								BEGIN
									INSERT @retTable
										SELECT	cId,
												cTreTpeId,
												cParent,
												cName,
												cFileName,
												cLocId,
												cLngId,
												cHideInMenu
										FROM	tblContTree
										WHERE	cId = @treId
								END
						END
						
					SELECT @searchId = @treId
					CLOSE get_parent
					DEALLOCATE get_parent
					
				END
				
			INSERT	@retTree
				SELECT	cId,
						cTreTpeId,
						cParent,
						cName,
						cFileName,
						cLocId,
						cLngId,
						cHideInMenu
				FROM	@retTable
			
			RETURN
		END
