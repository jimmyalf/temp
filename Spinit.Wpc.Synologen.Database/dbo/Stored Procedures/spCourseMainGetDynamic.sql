create PROCEDURE spCourseMainGetDynamic
					@categoryId INT,
					@searchString NVARCHAR(255),
					@orderBy NVARCHAR (255)
	AS
	
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	DECLARE @Trunc INT
	SET @Trunc = 20
	SELECT @sql=
			'SELECT 
			cmain.cId, 
			cmain.cName, 
			cmain.cDescription, 
			cmain.cDetail, 
			(SELECT COUNT(*) FROM tblCourse WHERE tblCourse.cCourseMainId = cmain.cId)  AS cCourses 
			FROM tblCourseMain cmain'

	IF (@categoryId IS NOT NULL AND @categoryId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblCourseMainCategoryConnection ON tblCourseMainCategoryConnection.cCourseMainId =  cmain.cId 
		WHERE tblCourseMainCategoryConnection.cCategoryId = @xCategoryId'
	END
	ELSE
	BEGIN					
		SELECT @sql = @sql + ' WHERE 1=1'
	END
	
    IF (@searchString IS NOT NULL AND LEN(@searchString) > 0)
    BEGIN
		SELECT @sql = @sql + ' AND (cName LIKE ''%''+@xSearchString+
		''%''OR cDescription LIKE ''%''+@xSearchString+''%''+
		''%''OR cDetail LIKE ''%''+@xSearchString+''%'')'
	END
	IF (@orderBy IS NOT NULL)
	BEGIN
		IF (SUBSTRING(@orderBy,1,12) = 'cDescription')
		BEGIN
			SET @orderBy = 'SUBSTRING(' + SUBSTRING(@orderBy,1,12) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@orderBy,13,LEN(@orderBy))
		END	
		ELSE IF (SUBSTRING(@orderBy,1,7) = 'cDetail')
		BEGIN
			SET @orderBy = 'SUBSTRING(' + SUBSTRING(@orderBy,1,7) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@orderBy,8,LEN(@orderBy))
		END	
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
		
	END
	SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xCategoryId INT'
						
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@searchString,
						@orderBy,
						@categoryId
