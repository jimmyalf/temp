create FUNCTION sfContGetTreeLeaves (@id INT)
	RETURNS
		@retTree TABLE (cId	INT)
	AS
		BEGIN

			DECLARE @leafId INT
			DECLARE getLeaf CURSOR LOCAL FOR
			SELECT cId FROM tblContTree
			WHERE cParent = @id
			
			OPEN getLeaf
			FETCH NEXT FROM getLeaf INTO @leafId
			
			WHILE (@@FETCH_STATUS <> -1)
			BEGIN
				INSERT INTO @retTree VALUES (@leafId)
				INSERT INTO @retTree SELECT * FROM sfContGetTreeLeaves(@leafId)
				FETCH NEXT FROM getLeaf INTO @leafId
			END
			CLOSE getLeaf
			DEALLOCATE getLeaf
			
RETURN
END
