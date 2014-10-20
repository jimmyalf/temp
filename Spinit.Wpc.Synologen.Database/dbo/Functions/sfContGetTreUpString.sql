CREATE FUNCTION sfContGetTreUpString (@treId INT,
									  @pgeId INT)
	RETURNS NVARCHAR (4000)
	AS
		BEGIN
			DECLARE	@retString NVARCHAR (4000),
					@tmpString NVARCHAR (4000),
					@first INT
					
			IF (@pgeId IS NOT NULL)
				BEGIN
					DECLARE get_tree CURSOR LOCAL FOR 
						SELECT	cId
						FROM	tblContTree
						WHERE	cPgeId = @pgeId
						
					OPEN get_tree
					FETCH NEXT FROM get_tree INTO @treId
					
					CLOSE get_tree
					DEALLOCATE get_tree
				END
					
			SELECT @first = 1
			SELECT @retString = NULL
			
			DECLARE get_string CURSOR LOCAL FOR
				SELECT	cName
				FROM	sfContGetTreUp (@treId)
				
			OPEN get_string
			FETCH NEXT FROM get_string INTO @tmpString
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
					IF (@first = 1)
						BEGIN
							SELECT @retString = @tmpString
							SELECT @first = 0
						END
					ELSE
						BEGIN
							SELECT @retString = @retString + ', ' + @tmpString
						END
					
					FETCH NEXT FROM get_string INTO @tmpString
				END
				
			CLOSE get_string
			DEALLOCATE get_string
			
			RETURN (@retString)
		END
