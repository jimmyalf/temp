CREATE PROCEDURE spContPgeChgPageContent
					@id INT,
					@content NTEXT,
					@size BIGINT,
					@changedBy NVARCHAR (100),
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@treTpeId INT,
					@pgeTpeId INT,
					@locId INT,
					@lngId INT
			
--			DECLARE get_type CURSOR LOCAL FOR
--				SELECT	cPgeTpeId
--				FROM	tblContPage
--				WHERE	cId = @id
--				
			SELECT @status = 0
--				
--			OPEN get_type
--			FETCH NEXT FROM get_type INTO @pgeTpeId
--			
--			IF (@@FETCH_STATUS <> -1)
--				BEGIN
--					IF (@pgeTpeId = 2)
--						BEGIN
--							EXECUTE spContChangeAllForTemplate @id, 
--															   @status OUTPUT
--						END
--				END
--				
--			CLOSE get_type
--			DEALLOCATE get_type
		
			UPDATE	tblContPage
			SET		cSize = @size,
					cContent = @content,
					cChangedBy = @changedBy,
					cChangedDate = GETDATE ()
			WHERE	cId = @id
			
			--UPDATE	tblContTree
			--SET		cPublishedDate = NULL
			--WHERE	cPgeId = @id
			--	OR	cTemplate = @id
			--	OR	cStylesheet = @id
			
			--DECLARE chk_def CURSOR LOCAL FOR
			--	SELECT	cLocId,
			--			cLngId
			--	FROM	tblContPageLocationLanguage
			--	WHERE	cPgeId = @id
			--		AND	cIsDefault = 1
					
			--OPEN chk_def
			--FETCH NEXT FROM chk_def INTO	@locId,
			--								@lngId
											
			--WHILE (@@FETCH_STATUS <> -1)
			--	BEGIN
			--		UPDATE	tblContTree
			--		SET		cPublishedDate = NULL
			--		WHERE	cLocId = @locId
			--			AND	cLngId = @lngId
			--			AND cTemplate IS NULL
			--	
			--		FETCH NEXT FROM chk_def INTO	@locId,
			--										@lngId
			--	END
				
			--CLOSE chk_def
			--DEALLOCATE chk_def
												
			IF (@status = 0)
				BEGIN
					SELECT @status = @@ERROR
				END
		END
