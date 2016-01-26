CREATE FUNCTION sfNewsGetAllCategory (@locationId INT, 
@languageId INT,
@defaultLanguageId INT)
RETURNS @categoryList table
(
	cCategoryId INT,
	cName NVARCHAR(255),
	cOrder INT
)	
AS
BEGIN
	DECLARE @inserted INT, @order INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @name NVARCHAR(255)	
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cResourceId, cOrder
			FROM	tblNewsCategory
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @order
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @name = cResource
		FROM tblNewsResources
		WHERE cId = @stringid AND cLanguageId = @languageid
		SET @inserted = @@ROWCOUNT
		IF (@inserted = 0)
		BEGIN
			SELECT @name = cResource
			FROM tblNewsResources
			WHERE cId = @stringid AND cLanguageId = @defaultLanguageId		
			SET @inserted = @@ROWCOUNT
		END
		IF (@inserted = 0)
		BEGIN
			SELECT TOP 1 @name = cResource
			FROM tblNewsResources
			WHERE cId = @stringid				
		END
		INSERT INTO @categoryList
		VALUES (@id, @name, @order)
		FETCH NEXT FROM getAll INTO @id, @stringid, @order
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
