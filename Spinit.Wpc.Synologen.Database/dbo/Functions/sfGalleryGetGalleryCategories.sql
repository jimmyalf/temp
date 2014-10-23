CREATE FUNCTION sfGalleryGetGalleryCategories (@galleryid INT,
@locationid INT, 
@languageid INT)
RETURNS @categoryList table
(
	cGalleryId int not null,
	cCategoryId int,
	cName nvarchar(255)
)	
AS
BEGIN
	DECLARE @defaultLanguageId INT
	IF (@locationid > 0)
	BEGIN
		SELECT @defaultLanguageId = cLanguageId
		FROM tblBaseLocationsLanguages
		WHERE cIsDefault = 1 AND cLocationId = @locationid
	END
	DECLARE @inserted INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @catid INT
	DECLARE @name NVARCHAR(255)
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cNameStringId, cCategoryId
			FROM	tblGalleryCategories tgc, tblGalleryCategoryConnection tgcc
			WHERE tgcc.cGalleryId = @galleryid
			AND tgcc.cCategoryId = tgc.cId
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @catid
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @name = cString
		FROM tblGalleryLanguageStrings
		WHERE cId = @stringid AND cLngId = @languageid
		SET @inserted = @@ROWCOUNT
		IF (@inserted = 0)
		BEGIN
			SELECT @name = cString
			FROM tblGalleryLanguageStrings
			WHERE cId = @stringid AND cLngId = @defaultLanguageId		
			SET @inserted = @@ROWCOUNT
		END
		IF (@inserted = 0)
		BEGIN
			SELECT TOP 1 @name = cString
			FROM tblGalleryLanguageStrings
			WHERE cId = @stringid				
		END
		INSERT INTO @categoryList
		VALUES (@galleryid, @catid, @name)
		FETCH NEXT FROM getAll INTO @id, @stringid, @catid		
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
