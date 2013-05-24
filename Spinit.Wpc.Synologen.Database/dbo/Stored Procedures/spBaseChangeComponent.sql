CREATE PROCEDURE spBaseChangeComponent
					@id INT,
					@name NVARCHAR (256),
					@compInstanceTable NVARCHAR (245),
					@description NVARCHAR (256),
					@fromComponentList BIT,
					@fromWysiwyg BIT,
					@framePage NVARCHAR (256),
					@editPage NVARCHAR (256),
					@external BIT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@dummy INT,
					@cmpName NVARCHAR (256),
					@cmpCompInstanceTable NVARCHAR (256),
					@cmpDescription NVARCHAR (256),
					@cmpFromComponentList BIT,
					@cmpFromWysiwyg BIT,
					@cmpFramePage NVARCHAR (256),
					@cmpEditPage NVARCHAR (256),
					@cmpExternal BIT
					
			IF ((@name IS NOT NULL) AND (LEN (@name) > 0))
				BEGIN
					DECLARE chk_name CURSOR LOCAL FOR
						SELECT	1
						FROM	tblBaseComponents
						WHERE	cName = @name
						
					OPEN chk_name
					FETCH NEXT FROM chk_name INTO @dummy
					
					IF (@@FETCH_STATUS <> -1)
						BEGIN
							CLOSE chk_name
							DEALLOCATE chk_name
							
							SET @status = -1
							RETURN
						END
						
					CLOSE chk_name
					DEALLOCATE chk_name
				END

			DECLARE get_component CURSOR LOCAL FOR
				SELECT	cName,
						cCompInstanceTable,
						cDescription,
						cFromComponentList,
						cFromWysiwyg,
						cFramePage,
						cEditPage,
						cExternal
				FROM	tblBaseComponents
				WHERE	cId = @id
		
			OPEN get_component
			FETCH NEXT FROM get_component INTO	@cmpName,
												@cmpCompInstanceTable,
												@cmpDescription,
												@cmpFromComponentList,
												@cmpFromWysiwyg,
												@cmpFramePage,
												@cmpEditPage,
												@cmpExternal
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_component
					DEALLOCATE get_component
					
					SET @status = -2
					RETURN
				END
				
			CLOSE get_component
			DEALLOCATE get_component
			
			IF (@name IS NOT NULL)
				BEGIN
					IF (LEN (@name) = 0)
						BEGIN
							SET @cmpName = NULL
						END
					ELSE
						BEGIN
							SET @cmpName = @name
						END
				END
				
			IF (@compInstanceTable IS NOT NULL)
				BEGIN
					IF (LEN (@compInstanceTable) = 0)
						BEGIN
							SET @cmpCompInstanceTable = NULL
						END
					ELSE
						BEGIN
							SET @cmpCompInstanceTable = @compInstanceTable
						END
				END
				
			IF (@description IS NOT NULL)
				BEGIN
					IF (LEN (@description) = 0)
						BEGIN
							SET @cmpDescription = NULL
						END
					ELSE
						BEGIN
							SET @cmpDescription = @description
						END
				END
			
			IF (@fromComponentList IS NOT NULL)
				BEGIN
					SET @cmpFromComponentList = @cmpFromComponentList
				END
				
			IF (@fromWysiwyg IS NOT NULL)
				BEGIN
					SET @cmpFromWysiwyg = @fromWysiwyg
				END
				
			IF (@framePage IS NOT NULL)
				BEGIN
					IF (LEN (@framePage) = 0)
						BEGIN
							SET @cmpFramePage = NULL
						END
					ELSE
						BEGIN
							SET @cmpFramePage = @framePage
						END
				END
				
			IF (@editPage IS NOT NULL)
				BEGIN
					IF (LEN (@editPage) = 0)
						BEGIN
							SET @cmpEditPage = NULL
						END
					ELSE
						BEGIN
							SET @cmpEditPage = @editPage
						END
				END
				
			IF (@external IS NOT NULL)
				BEGIN
					SET @cmpExternal = @external
				END
				
			UPDATE	tblBaseComponents
			SET		cName = @cmpName,
					cCompInstanceTable = @compInstanceTable,
					cDescription = @cmpDescription,
					cFromComponentList = @cmpFromComponentList,
					cFromWysiwyg = @cmpFromWysiwyg,
					cFramePage = @cmpFramePage,
					cEditPage = @cmpEditPage,
					cExternal = @cmpExternal
			WHERE	cId = @id
			
			SET @status = @@ERROR
		END
