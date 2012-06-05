CREATE FUNCTION sfCourseGetAllCategory
(@locationId INT, @languageId INT, @defaultLanguageId INT)
RETURNS 
    @categoryList TABLE (
        [cCategoryId] INT            NULL,
        [cName]       NVARCHAR (255) NULL,
        [cOrder]      INT            NULL)
AS
BEGIN
	DECLARE @inserted INT, @order INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @name NVARCHAR(255)	
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cResourceId, cOrder
			FROM	tblCourseCategory
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @order
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @name = cResource
		FROM tblCourseResource
		WHERE cId = @stringid AND cLanguageId = @languageid
		SET @inserted = @@ROWCOUNT
		IF (@inserted = 0)
		BEGIN
			SELECT @name = cResource
			FROM tblCourseResource
			WHERE cId = @stringid AND cLanguageId = @defaultLanguageId		
			SET @inserted = @@ROWCOUNT
		END
		IF (@inserted = 0)
		BEGIN
			SELECT TOP 1 @name = cResource
			FROM tblCourseResource
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
