CREATE PROCEDURE spContTreeFindTemplate
					@type INT,
					@treeId INT,
					@result INT OUTPUT,
					@status INT OUTPUT
	AS
	BEGIN
		DECLARE @id INT, @template INT
		SET @template = 0
		
		DECLARE getTreeUp CURSOR LOCAL FOR
		SELECT cId FROM sfContGetTreUp(@treeId)
		
		OPEN getTreeUp
		
		FETCH NEXT FROM getTreeUp INTO @id
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			IF (@type = 2)
			BEGIN
				SELECT @template = cTemplate
				FROM tblContTree 
				WHERE cId = @id
			
			END
			IF (@type= 4)
			BEGIN
				SELECT @template = cStylesheet 
				FROM tblContTree 
				WHERE cId = @id
			
			END
			IF (@template > 0)
			BEGIN
				BREAK
			END
			FETCH NEXT FROM getTreeUp INTO @id
		END
		CLOSE getTreeUp
		DEALLOCATE getTreeUp
		
		IF (@template > 0)
		BEGIN
			SET @result = @template
		END
		ELSE
		BEGIN
			SET @result = 0
		END
		SELECT @status = @@ERROR

	END
