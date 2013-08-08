CREATE FUNCTION sfMemberGetAllMembersCategories (@locationid INT, 
@languageid INT)
RETURNS @categoryList table
(
	cCategoryId int,
	cName nvarchar(255),
	cOrderId int,
	cBaseGroupId int
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
	DECLARE @inserted INT, @order INT, @baseGroupId INT
	DECLARE @id INT
	DECLARE @stringid INT
	DECLARE @name NVARCHAR(255)	
	DECLARE getAll CURSOR LOCAL FOR	
			SELECT	cId, cNameStringId, cOrderId, cBaseGroupId
			FROM	tblMemberCategories tmc
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id, @stringid, @order, @baseGroupId
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN
		SELECT @name = cString
		FROM tblMemberLanguageStrings
		WHERE cId = @stringid AND cLngId = @languageid
		SET @inserted = @@ROWCOUNT
		IF (@inserted = 0)
		BEGIN
			SELECT @name = cString
			FROM tblMemberLanguageStrings
			WHERE cId = @stringid AND cLngId = @defaultLanguageId		
			SET @inserted = @@ROWCOUNT
		END
		IF (@inserted = 0)
		BEGIN
			SELECT TOP 1 @name = cString
			FROM tblMemberLanguageStrings
			WHERE cId = @stringid				
		END
		INSERT INTO @categoryList
		VALUES (@id, @name, @order, @baseGroupId)
		FETCH NEXT FROM getAll INTO @id, @stringid, @order, @baseGroupId
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
