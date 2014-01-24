create PROCEDURE spNewsGetNewsAlternative
					@newsId INT = 0,
					@locationId INT = 0,
					@categoryId INT = 0,
					@languageId INT = 0,
					@daysBack INT = 0,
					@groupId INT = 0,
					@userId INT = -1,
					@authorId INT = 0,
					@maxNumberOfRows INT = 0				
	AS BEGIN
	DECLARE @sql nvarchar(4000), @paramlist	nvarchar(4000)
	SELECT @sql = 'SELECT' 
			IF (@maxNumberOfRows IS NOT NULL AND @maxNumberOfRows > 0) BEGIN
				SELECT @sql = @sql + ' TOP ' + CAST(@maxNumberOfRows AS NVARCHAR(5))
			END
			SELECT @sql = @sql + ' *, dbo.sfNewsIsRead(@xNewsId, vwNewsIntranetNews.cId) AS IsRead  FROM vwNewsIntranetNews'
			IF (@locationId IS NOT NULL AND @locationId > 0) BEGIN
				SELECT @sql = @sql + ' ,tblNewsLocationConnection tnlc'
			END						
			IF (@languageId IS NOT NULL AND @languageId > 0) BEGIN
				SELECT @sql = @sql + ' ,tblNewsLanguageConnection tnlgc'
			END				
			IF (@categoryId IS NOT NULL AND @categoryId > 0) BEGIN
				SELECT @sql = @sql + ' ,tblNewsCategoryConnection tncc'
			END					
			IF (@groupId IS NOT NULL AND @groupId > 0) BEGIN
				SELECT @sql = @sql + ' ,tblNewsGroupConnection tngc'
			END						 
			SELECT @sql = @sql + 
				' WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
				AND (cEndDate >= getDate() OR cEndDate IS NULL)
				AND cApprovedDate IS NOT NULL'
			IF (@newsId IS NOT NULL AND @newsId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cId = @xNewsId'
			END
			IF (@categoryId IS NOT NULL AND @categoryId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cId = tncc.cNewsId
				AND tncc.cCategoryId = @xCategoryId'
			END
			IF (@languageId IS NOT NULL AND @languageId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cId = tnlgc.cNewsId
				AND tnlgc.cLanguageId = @xLanguageId'
			END		
			IF (@locationId IS NOT NULL AND @locationId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cId = tnlc.cNewsId 
				AND tnlc.cLocationId = @xLocationId'
			END
			IF (@groupId IS NOT NULL AND @groupId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cId = tngc.cNewsId
				AND tngc.cGroupId = @xGroupId'
			END
			IF (@userId IS NOT NULL AND @userId > 0) BEGIN
				SELECT @sql = @sql + ' AND cId IN (SELECT cId FROM sfNewsAllowedNews(@xUserId))'
			END
			IF (@authorId IS NOT NULL AND @authorId > 0) BEGIN
				SELECT @sql = @sql + ' AND vwNewsIntranetNews.cCreatedBy = (SELECT cUserName FROM tblBaseUsers WHERE cId = @xAuthorId)'
			END
			SELECT @sql = @sql + ' 
				ORDER BY cStartDate DESC'
		END
		
	SELECT @paramlist = '@xNewsId INT, 
						@xCategoryId INT, 
						@xLanguageId INT, 
						@xLocationId INT,
						@xGroupId INT,
						@xUserId INT,
						@xAuthorId INT'
						
						
	EXEC sp_executesql @sql,@paramlist,                                 
						@newsId,
						@categoryId,
						@languageId,
						@locationId,
						@groupId,
						@userId,
						@authorId
