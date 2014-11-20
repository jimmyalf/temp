CREATE FUNCTION sfCourseGetCategory
(@MainId INT, @locationId INT, @languageId INT, @defaultLanguageId INT)
RETURNS 
    @categoryList TABLE (
        [cMainId]     INT            NOT NULL,
        [cCategoryId] INT            NULL,
        [cName]       NVARCHAR (255) NULL)
AS
BEGIN
	DECLARE @inserted INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @catid INT
	DECLARE @name NVARCHAR(255)
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cResourceId, cCategoryId
			FROM	tblCourseCategory tjc, tblCourseMainCategoryConnection tjcc
			WHERE tjcc.cCourseMainId = @MainId
			AND tjcc.cCategoryId = tjc.cId
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @catid
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
		VALUES (@MainId, @catid, @name)
		FETCH NEXT FROM getAll INTO @id, @stringid, @catid		
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
