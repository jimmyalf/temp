CREATE FUNCTION sfNewsGetCategory (@newsId INT,
@locationId INT, 
@languageId INT,
@defaultLanguageId INT)
RETURNS @categoryList table
(
	cNewsId INT NOT NULL,
	cCategoryId INT,
	cName NVARCHAR(255)
)	
AS
BEGIN
	DECLARE @inserted INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @catid INT
	DECLARE @name NVARCHAR(255)
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cResourceId, cCategoryId
			FROM	tblNewsCategory tnc, tblNewsCategoryConnection tncc
			WHERE tncc.cNewsId = @newsId
			AND tncc.cCategoryId = tnc.cId
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @catid
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
		VALUES (@newsId, @catid, @name)
		FETCH NEXT FROM getAll INTO @id, @stringid, @catid		
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
