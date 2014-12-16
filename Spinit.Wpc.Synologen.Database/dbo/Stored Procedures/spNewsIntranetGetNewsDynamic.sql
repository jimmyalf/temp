create PROCEDURE spNewsIntranetGetNewsDynamic
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@OnlyPublished BIT,
					@GroupId INT,
					@PublicUserId INT,
					@AuthorId INT,
					@OnlyUnread BIT
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
				news.cLockedDate,
				news.Author,
				news.AuthorUserId,
				dbo.sfNewsIsRead(@xPublicUserId,news.cId) AS IsRead	
				FROM vwNewsIntranetNews news'
	IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@LanguageId IS NOT NULL AND @LanguageId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0) AND
		(@GroupId IS NOT NULL AND @GroupId > 0) )
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblNewsLocationConnection ON news.cId = tblNewslocationConnection.cNewsId 
		INNER JOIN tblNewsLanguageConnection ON news.cId = tblNewsLanguageConnection.cNewsId 
		INNER JOIN tblNewsCategoryConnection ON news.cId = tblNewsCategoryConnection.cNewsId
		INNER JOIN tblNewsGroupConnection ON news.cId = tblNewsGroupConnection.cNewsId 
		WHERE tblNewsLocationConnection.cLocationid = @xLocationId AND 
		tblNewsLanguageConnection.cLanguageId = @xLanguageId AND 
		tblNewsCategoryConnection.cCategoryId = @xCategoryId AND 
		tblNewsGroupConnection.cGroupId = @xGroupId'
	END
	ELSE IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
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
	
	IF (@PublicUserId IS NOT NULL AND @PublicUserId > 0 AND @OnlyPublished IS NOT NULL AND @OnlyPublished = 1)
	BEGIN
		SELECT @sql = @sql + ' AND cId IN (SELECT cId FROM sfNewsAllowedNews(@xPublicUserId))'
	END
	
	IF (@AuthorId IS NOT NULL AND @AuthorId > 0)
	BEGIN
		SELECT @sql = @sql + ' AND news.cCreatedBy = (SELECT cUserName FROM tblBaseUsers WHERE cId = @xAuthorId)'
	END
	
	IF (@OnlyPublished IS NOT NULL AND @OnlyPublished = 1)
	BEGIN
		SELECT @sql = @sql + ' AND (cStartDate <= getDate() OR cStartDate IS NULL)' +
								'AND (cEndDate >= getDate() OR cEndDate IS NULL)' +
								'AND cApprovedDate IS NOT NULL'
	END
	IF (@OnlyUnread IS NOT NULL AND @OnlyUnread = 1 AND @PublicUserId IS NOT NULL AND @PublicUserId > 0)
	BEGIN
		SELECT @sql = @sql + ' AND news.cId NOT IN (SELECT cNewsId FROM tblNewsRead' +
							' WHERE cReadBy=(SELECT cUserName FROM tblBaseUsers WHERE cId = @xPublicUserId))'
	END
	
	SELECT @sql = @sql + ' ORDER BY cStartDate DESC'
		
	
	SELECT @paramlist = '@xPublicUserId INT,@xAuthorId INT,@xOnlyPublished BIT,@xOnlyRead BIT' +
						',@xLocationId INT, @xLanguageId INT, @xCategoryId INT, @xGroupId INT'
					
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@PublicUserId,
						@AuthorId,
						@OnlyPublished,
						@OnlyUnread,
						@LocationId,
						@LanguageId,
						@CategoryId,
						@GroupId
