create FUNCTION sfContGetFileNameDownString (@treId INT)
RETURNS NVARCHAR (4000)
AS
	BEGIN
		DECLARE	@retString NVARCHAR (4000),
				@tmpString NVARCHAR (256),
				@first INT,
				@id INT,
				@locId INT,
				@lngId INT,
				@treTpeId INT,
				@parTreTpeId INT,
				@noOfLngs INT,
				@isDefault BIT,
				@link NVARCHAR (256)
								
		SET @first = 1
		SET @retString = NULL
		
		SET @id = @treId
		
		DECLARE get_tree CURSOR LOCAL FOR
			SELECT	tre.cLocId,
					tre.cLngId,
					tre.cTreTpeId,
					par.cTreTpeId AS ParTreTpeId,
					tre.cLink
			FROM	tblContTree tre
				INNER JOIN tblContTree par
					ON tre.cParent = par.cId
			WHERE	tre.cId = @id
			
		OPEN get_tree
		FETCH NEXT FROM get_tree INTO	@locId,
										@lngId,
										@treTpeId,
										@parTreTpeId,
										@link
										
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_tree
				DEALLOCATE get_tree
				
				DECLARE get_tree_loc CURSOR LOCAL FOR
					SELECT	tre.cLocId,
							tre.cLngId,
							tre.cTreTpeId,
							0 AS ParTreTpeId,
							tre.cLink
					FROM	tblContTree tre
					WHERE	tre.cId = @treId
						AND tre.cTreTpeId = 1
					
				OPEN get_tree_loc
				FETCH NEXT FROM get_tree_loc INTO	@locId,
													@lngId,
													@treTpeId,
													@parTreTpeId,
													@link
				
				
				IF (@@FETCH_STATUS = -1)
					BEGIN
						CLOSE get_tree_loc
						DEALLOCATE get_tree_loc
				
						RETURN (NULL)
					END
					
				CLOSE get_tree_loc
				DEALLOCATE get_tree_loc
			END
		ELSE
			BEGIN
				CLOSE get_tree
				DEALLOCATE get_tree
			END
		
		IF (@treTpeId = 1)
			BEGIN
				DECLARE get_default CURSOR LOCAL FOR 
					SELECT	tre.cId,
							tre.cLocId,
							tre.cLngId,
							tre.cTreTpeId,
							par.cTreTpeId AS ParTreTpeId,
							tre.cLink
					FROM	tblContTree tre
						INNER JOIN tblContTree par
							ON tre.cParent = par.cId
					WHERE	tre.cLocId = @locId
						AND	tre.cTreTpeId = 5
						AND par.cTreTpeId = 2
						AND par.cLngId = (SELECT	cLanguageId
										  FROM		tblBaseLocationsLanguages
										  WHERE		cLocationId = @locId
											AND		cIsDefault = 1)
											
				OPEN get_default
				FETCH NEXT FROM get_default INTO	@id,
													@locId,
													@lngId,
													@treTpeId,
													@parTreTpeId,
													@link
													
				IF (@@FETCH_STATUS = -1)
					BEGIN
						CLOSE get_default
						DEALLOCATE get_default
						
						RETURN (NULL)
					END
					
				CLOSE get_default
				DEALLOCATE get_default
			END
			
		IF (@treTpeId = 2) OR (@treTpeId = 3)
			BEGIN
				DECLARE get_default CURSOR LOCAL FOR 
					SELECT	tre.cId,
							tre.cLocId,
							tre.cLngId,
							tre.cTreTpeId,
							par.cTreTpeId AS ParTreTpeId,
							tre.cLink
					FROM	tblContTree tre
						INNER JOIN tblContTree par
							ON tre.cParent = par.cId
					WHERE	tre.cParent = @id
						AND	tre.cTreTpeId = 5
				
				OPEN get_default
				FETCH NEXT FROM get_default INTO	@id,
													@locId,
													@lngId,
													@treTpeId,
													@parTreTpeId,
													@link
													
				IF (@@FETCH_STATUS = -1)
					BEGIN
						CLOSE get_default
						DEALLOCATE get_default
						
						RETURN (NULL)
					END
					
				CLOSE get_default
				DEALLOCATE get_default
			END
			
		IF (@treTpeId = 9) OR (@treTpeId = 10) OR (@treTpeId = 17)
			BEGIN
				RETURN (NULL)
			END
			
		IF (@treTpeId = 12)
			BEGIN
				RETURN (@link)
			END
		
		DECLARE get_no_lngs CURSOR LOCAL FOR
			SELECT	cIsDefault,
					'Lngs' =
						(SELECT	COUNT (cLanguageId)
						 FROM	tblBaseLocationsLanguages
						 WHERE	cLocationId = @locId)
			FROM	tblBaseLocationsLanguages
			WHERE	cLocationId = @locId
				AND	cLanguageId = @lngId
												
		OPEN get_no_lngs
		FETCH NEXT FROM get_no_lngs INTO	@isDefault,
											@noOfLngs
											
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_no_lngs
				DEALLOCATE get_no_lngs
				
				RETURN (NULL)
			END
			
		CLOSE get_no_lngs
		DEALLOCATE get_no_lngs
		
		IF (@treTpeId = 5) AND (@parTreTpeId = 2)
			BEGIN
				IF (@isDefault = 1)
					BEGIN
						RETURN ('\index.aspx')
					END
			END
		
		DECLARE get_string CURSOR LOCAL FOR
			SELECT	cFileName,
					cTreTpeId
			FROM	sfContGetTreUp (@id)
			
		OPEN get_string
		FETCH NEXT FROM get_string INTO	@tmpString,
										@treTpeId
		
		WHILE (@@FETCH_STATUS <> -1)
			BEGIN
				IF (@treTpeId = 9) OR (@treTpeId = 10)
					BEGIN
						RETURN (NULL)
					END

				IF (@treTpeId = 2) AND (@noOfLngs = 1)
					BEGIN
						BREAK
					END
			
				IF (@first = 1)
					BEGIN
						SET @retString = @tmpString + '.aspx'
						SET @first = 0
					END
				ELSE
					BEGIN
						SET @retString =  @tmpString + '\' + @retString
					END
				
				FETCH NEXT FROM get_string INTO	@tmpString,
												@treTpeId
			END
			
		CLOSE get_string
		DEALLOCATE get_string
				
		RETURN ('\' + @retString)
	END
