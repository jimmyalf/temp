CREATE PROCEDURE spCourseGetDynamic
					@mainId INT,
					@languageId INT,
					@locationId INT,
					@cityId INT,
					@searchString NVARCHAR(255),
					@orderBy NVARCHAR (255)
	AS
	
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	DECLARE @Trunc INT
	SET @Trunc = 20
	SELECT @sql=
			'SELECT 
			course.cId, 
			course.cHeading, 
			course.cContactName, 
			course.cLastApplicationDate, 
			course.cCourseStartDate, 
			course.cCourseEndDate, 
			course.cPublishStartDate, 
			course.cPublishEndDate, 
			(SELECT cCity FROM tblCourseCity WHERE tblCourseCity.cId = course.cCityId) AS cCityName, 
			(SELECT COUNT(*) FROM tblCourseApplication WHERE tblCourseApplication.cCourseId = course.cId)  AS cApplications, 
			dbo.sfCourseGetNoOfParticipants(course.cId) AS cNoOfParticipants 
			FROM tblCourse course 
			INNER JOIN tblCourseCity ON tblCourseCity.cId = course.cCityId'

	IF ((@locationId IS NOT NULL AND @locationId > 0) AND
		(@languageId IS NOT NULL AND @languageId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblCourseLocationConnection ON course.cId = tblCourseLocationConnection.cCourseId 
		INNER JOIN tblCourseLanguageConnection ON course.cId = tblCourseLanguageConnection.cCourseId 
		WHERE tblCourseLocationConnection.cLocationId = @xLocationId AND 
		tblCourseLanguageConnection.cLanguageId = @xLanguageId'
	END				
	ELSE IF (@locationId IS NOT NULL AND @locationId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblCourseLocationConnection ON course.cId = tblCourseLocationConnection.cCourseId 
		WHERE tblCourseLocationConnection.cLocationid = @xLocationId'
	END
	ELSE IF (@languageId IS NOT NULL AND @languageId > 0)
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblCourseLanguageConnection ON course.cId = tblCourseLanguageConnection.cCourseId 
		WHERE tblCourseLanguageConnection.cLanguageId = @xLanguageId'
	END
	ELSE
	BEGIN					
		SELECT @sql = @sql + ' WHERE 1=1'
	END
	--filter without JOINS
	IF (@mainId IS NOT NULL AND @mainId > 0)
	BEGIN
		SELECT @sql = @sql +  ' AND course.cCourseMainId = @xCourseMainId'
	END
	IF (@cityId IS NOT NULL AND @cityId > 0)
	BEGIN
		SELECT @sql = @sql + ' AND tblCourseCity.cId = @xCityId'
	END
	
    IF (@searchString IS NOT NULL AND LEN(@searchString) > 0)
    BEGIN
		SELECT @sql = @sql +	' AND (tblCourseCity.cCity LIKE ''%''+@xSearchString+
								''%''OR cHeading LIKE ''%''+@xSearchString+''%''+
								''%''OR cLastApplicationDate LIKE ''%''+@xSearchString+''%''+
								''%''OR cCourseStartDate LIKE ''%''+@xSearchString+''%''+																
								''%''OR cContactName LIKE ''%''+@xSearchString+''%'')'
	END
	IF (@orderBy IS NOT NULL)
	BEGIN
		IF (SUBSTRING(@orderBy,1,8) = 'cHeading' OR SUBSTRING(@orderBy,1,8) = 'cSummary')
		BEGIN
			SET @orderBy = 'SUBSTRING(' + SUBSTRING(@orderBy,1,8) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@orderBy,9,LEN(@orderBy))
		END
		SELECT @sql = @sql + ' ORDER BY ' + @orderBy
		
	END
	SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xCityId INT,@xLanguageId INT,@xLocationId INT,@xCourseMainId INT'
						
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@searchString,
						@orderBy,
						@cityId,
						@languageId,
						@locationId,
						@mainId
