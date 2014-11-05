
CREATE PROCEDURE spMemberGetNews
					@newsId INT,
					@locationId INT,
					@categoryId INT,
					@languageId INT,
					@memberId INT,
					@status INT OUTPUT
AS
	BEGIN
			SELECT *
			FROM	tblNews, tblNewsLocationConnection tnlc, 
			tblNewsLanguageConnection tnlgc,
			tblNewsCategoryConnection tncc,
			tblMemberNews tmn
			
			WHERE (cStartDate <= getDate() OR cStartDate IS NULL)
			AND (cEndDate >= getDate() OR cEndDate IS NULL)
			AND cApprovedDate IS NOT NULL

			AND tblNews.cId = tnlc.cNewsId
			AND tblNews.cId = tnlgc.cNewsId
			AND tblNews.cId = tncc.cNewsId
			AND tblNews.cId = tmn.cNewsId

			AND tncc.cCategoryId = @categoryId
			AND tnlgc.cLanguageId = @languageId
			AND tnlc.cLocationId = @locationId
			AND tmn.cMemberId = @memberId 
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
