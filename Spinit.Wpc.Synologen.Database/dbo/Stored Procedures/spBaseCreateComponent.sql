CREATE PROCEDURE spBaseCreateComponent
					@name NVARCHAR (256),
					@compInstanceTable NVARCHAR (256),
					@description NVARCHAR (256),
					@fromComponentList BIT,
					@fromWysiwyg BIT,
					@framePage NVARCHAR (256),
					@editPage NVARCHAR (256),
					@external BIT,
					@id INT OUTPUT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@dummy INT
			
			DECLARE chk_name CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseComponents
				WHERE	cName = @name
				
			OPEN chk_name
			FETCH NEXT FROM  chk_name INTO @dummy
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_name
					DEALLOCATE chk_name
					
					SET @status = -1
					RETURN
				END
				
			CLOSE chk_name
			DEALLOCATE chk_name
			
			INSERT INTO tblBaseComponents
				(cName, cCompInstanceTable, cDescription, cFromComponentList,
				 cFromWysiwyg, cFramePage, cEditPage, cExternal)
			VALUES
				(@name, @compInstanceTable, @description, @fromComponentList,
				 @fromWysiwyg, @framePage, @editPage, @external)
				 
			IF (@@ERROR <> 0)
				BEGIN
					SET @id = 0
				END
			ELSE
				BEGIN
					SET @id = @@IDENTITY
				END
				
			SET @status = @@ERROR
		END
