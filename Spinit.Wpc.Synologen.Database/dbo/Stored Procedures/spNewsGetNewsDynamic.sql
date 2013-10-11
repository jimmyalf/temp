CREATE PROCEDURE spNewsGetNewsDynamic
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
	AS
	
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	DECLARE @Trunc INT
	SET @Trunc = 20
	SELECT @sql=
			'SELECT 
				news.cId,
				news.cNewsType,
				news.cHeading, 
				news.cSummary,
				news.cBody,
				news.cFormatedBody,
				news.cExternalLink,
				news.cSpotImage,
				news.cSpotHeight,
				news.cSpotWidth,
				news.cSpotAlign,
				news.cStartDate,
				news.cEndDate,
				news.cCreatedBy,
				news.cCreatedDate,
				news.cEditedBy,
				news.cEditedDate,
				news.cApprovedBy,
				news.cApprovedDate,
				news.cLockedBy,
				news.cLockedDate	
				FROM tblNews news'
	IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@LanguageId IS NOT NULL AND @LanguageId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblNewsLocationConnection ON news.cId = tblNewslocationConnection.cNewsId 
		INNER JOIN tblNewsLanguageConnection ON news.cId = tblNewsLanguageConnection.cNewsId 
		INNER JOIN tblNewsCategoryConnection ON news.cId = tblNewsCategoryConnection.cNewsId 
		WHERE tblNewsLocationConnection.cLocationid = @xLocationId AND 
		tblNewsLanguageConnection.cLanguageId = @xLanguageId AND 
		tblNewsCategoryConnection.cCategoryId = @xCategoryId'
	END				   
	ELSE IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@LanguageId IS NOT NULL AND @LanguageId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblNewsLocationConnection ON news.cId = tblNewslocationConnection.cNewsId 
		INNER JOIN tblNewsLanguageConnection ON news.cId = tblNewsLanguageConnection.cNewsId 
		WHERE tblNewsLocationConnection.cLocationid = @xLocationId AND 
		tblNewsLanguageConnection.cLanguageId = @xLanguageId'
	END				
	ELSE IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblNewsLocationConnection ON news.cId = tblNewslocationConnection.cNewsId 
		INNER JOIN tblNewsCategoryConnection ON news.cId = tblNewsCategoryConnection.cNewsId 
		WHERE tblNewsLocationConnection.cLocationid = @xLocationId AND 
		tblNewsCategoryConnection.cCategoryId = @xCategoryId'
	END
	ELSE IF ((@LanguageId IS NOT NULL AND @LanguageId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblNewsLanguageConnection ON news.cId = tblNewsLanguageConnection.cNewsId 
		INNER JOIN tblNewsCategoryConnection ON news.cId = tblNewsCategoryConnection.cNewsId 
		WHERE tblNewsLanguageConnection.cLanguageId = @xLanguageId AND 
		tblNewsCategoryConnection.cCategoryId = @xCategoryId'
	END
	ELSE IF (@LocationId IS NOT NULL AND @LocationId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblNewsLocationConnection ON news.cId = tblNewslocationConnection.cNewsId 
		WHERE tblNewsLocationConnection.cLocationid = @xLocationId'
	END
	ELSE IF (@LanguageId IS NOT NULL AND @LanguageId > 0)
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblNewsLanguageConnection ON news.cId = tblNewsLanguageConnection.cNewsId 
		WHERE tblNewsLanguageConnection.cLanguageId = @xLanguageId'
	END
	ELSE IF (@CategoryId IS NOT NULL AND @CategoryId > 0)
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblNewsCategoryConnection ON news.cId = tblNewsCategoryConnection.cNewsId 
		WHERE tblNewsCategoryConnection.cCategoryid = @xCategoryId'
	END
	ELSE
	BEGIN					
		SELECT @sql = @sql + ' WHERE 1=1'
	END
	
    IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
    BEGIN
		SELECT @sql = @sql + ' AND (cHeading LIKE ''%''+@xSearchString+
		''%''OR cSummary LIKE ''%''+@xSearchString+''%''+
		''%''OR cFormatedBody LIKE ''%''+@xSearchString+''%''+
		''%''OR cBody LIKE ''%''+@xSearchString+''%'')'
	END
	IF (@OrderBy IS NOT NULL)
	BEGIN
		IF (SUBSTRING(@OrderBy,1,8) = 'cHeading' OR SUBSTRING(@OrderBy,1,8) = 'cSummary')
		BEGIN
			SET @OrderBy = 'SUBSTRING(' + SUBSTRING(@OrderBy,1,8) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@OrderBy,9,LEN(@OrderBy))
		END
		IF (SUBSTRING(@OrderBy,1,5) = 'cBody')
		BEGIN
			SET @OrderBy = 'SUBSTRING(' + SUBSTRING(@OrderBy,1,5) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@OrderBy,6,LEN(@OrderBy))
		END
		IF (SUBSTRING(@OrderBy,1,13) = 'cFormatedBody')
		BEGIN
			SET @OrderBy = 'SUBSTRING(' + SUBSTRING(@OrderBy,1,13) + ', 1,' + CONVERT(NVARCHAR(10),@Trunc) + ') ' + SUBSTRING(@OrderBy,14,LEN(@OrderBy))
		END		
		SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
		
	END
	SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xLocationId INT, @xLanguageId INT, @xCategoryId INT'
						
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@SearchString,
						@OrderBy,
						@LocationId,
						@LanguageId,
						@CategoryId
