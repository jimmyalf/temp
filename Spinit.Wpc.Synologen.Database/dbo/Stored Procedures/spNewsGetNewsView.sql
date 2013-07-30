create PROCEDURE spNewsGetNewsView
					@type INT,
					@newsId INT,
					@locationId INT,
					@categoryId INT,
					@languageId INT,
					@daysBack INT = 0,
					@groupId INT = 0,
					@userId INT = -1,
					@authorId INT = 0,
					@status INT OUTPUT
	AS
		IF (@type = 0) 
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews
			ORDER BY cStartDate DESC
		END
		IF (@type = 1)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews
			WHERE cId = @newsId
			ORDER BY cStartDate DESC
		END
		IF (@type = 2)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc
			WHERE vwNewsIntranetNews.cId = tnlc.cNewsId
			AND tnlc.cLocationId = @locationId
			ORDER BY cStartDate DESC		
		END
			
		IF (@type = 3)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsCategoryConnection tncc
			WHERE vwNewsIntranetNews.cId = @newsId
			AND tncc.cNewsId = @newsId
			AND tncc.cCategoryId = @categoryId
			ORDER BY cStartDate DESC
		END
		
		IF (@type = 4)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsCategoryConnection tncc, tblNewsLocationConnection tnlc
			WHERE vwNewsIntranetNews.cId = tncc.cNewsId
			AND vwNewsIntranetNews.cId = tnlc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlc.cLocationId = @locationId
			ORDER BY cStartDate DESC		
		END
		IF (@type = 5)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLanguageConnection tnlc, tblNewsLocationConnection tnloc
			WHERE vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnloc.cNewsId
			AND tnlc.cLanguageId = @languageId
			AND tnloc.cLocationId = @locationId
			ORDER BY cStartDate DESC		
		END
		IF (@type = 6)
		BEGIN
			SELECT *,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc
			
			WHERE vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnlgc.cNewsId
			AND vwNewsIntranetNews.cId = tncc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			ORDER BY cStartDate DESC
		END
		IF (@type = 7) 
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC
		END
		IF (@type = 8) 
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews , tblNewsLocationConnection tnlc
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND tnlc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC			
		END
		IF (@type = 9)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsCategoryConnection tncc, tblNewsLocationConnection tnlc
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND vwNewsIntranetNews.cId = tncc.cNewsId
			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND tncc.cCategoryId = @categoryId
			AND tnlc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC		
		END
		IF (@type = 10)
		BEGIN				
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsCategoryConnection tncc, tblNewsLocationConnection tnlc
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND vwNewsIntranetNews.cId = tncc.cNewsId
			AND vwNewsIntranetNews.cId = tnlc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC		
		END
		IF (@type = 11)
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead

			FROM	vwNewsIntranetNews, tblNewsLanguageConnection tnlc, tblNewsLocationConnection tnloc
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL

			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnloc.cNewsId

			AND tnlc.cLanguageId = @languageId
			AND tnloc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC		
		END
		
		if (@type = 12)
		BEGIN
			SELECT *,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc
			
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL

			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnlgc.cNewsId
			AND vwNewsIntranetNews.cId = tncc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC
		END
		
		IF (@type = 13) 
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews
			WHERE cStartDate > DATEADD (day, -1 * @daysBack, getDate ())
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC
		END
		
		IF (@type = 14) 
		BEGIN
			SELECT	*,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews , tblNewsLocationConnection tnlc
			WHERE cStartDate > DATEADD (day, -1 * @daysBack, getDate ())
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL
			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND tnlc.cLocationId = @locationId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC			
		END
		
		IF (@type = 15)
		BEGIN
			SELECT *,  dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc,
			tblNewsGroupConnection tngc
			
			WHERE vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnlgc.cNewsId
			AND vwNewsIntranetNews.cId = tncc.cNewsId
			AND vwNewsIntranetNews.cId = tngc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			AND tngc.cGroupId = @groupId
			ORDER BY cStartDate DESC
		END
		
		if (@type = 16)
		BEGIN
			SELECT *, dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc,
			tblNewsGroupConnection tngc
			
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL

			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnlgc.cNewsId
			AND vwNewsIntranetNews.cId = tncc.cNewsId
			AND vwNewsIntranetNews.cId = tngc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			AND tngc.cGroupId = @groupId
			AND cId IN (SELECT cId FROM sfNewsAllowedNews(@userId))
			ORDER BY cStartDate DESC
		END
		
		if (@type = 17)
		BEGIN
			SELECT *, dbo.sfNewsIsRead(@userId,vwNewsIntranetNews.cId) AS IsRead
			FROM	vwNewsIntranetNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc
			
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL

			AND vwNewsIntranetNews.cId = tnlc.cNewsId
			AND vwNewsIntranetNews.cId = tnlgc.cNewsId
			AND vwNewsIntranetNews.cId = tncc.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			AND vwNewsIntranetNews.cCreatedBy = (SELECT cUserName FROM tblBaseUsers WHERE cId = @authorId)
			ORDER BY cStartDate DESC
		END
		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
