
CREATE PROCEDURE spCampaignGetCampaigns
					@type INT,
					@campaignId INT,
					@categoryId INT,
					@locationId INT,
					@languageId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
				SELECT * 
				FROM fnGetAllCampaigns(1, @Locationid, @Languageid) g
				WHERE cId = @campaignId
			END
			IF (@type = 1)
			BEGIN
				SELECT g.*
				FROM fnGetAllCampaigns(1, @Locationid, @Languageid) g
				INNER JOIN tblCampaignLocationConnection loc
				ON loc.cCampaignId=g.cId
				INNER JOIN tblCampaignLanguageConnection lang
				ON lang.cCampaignId=g.cId
				INNER JOIN tblCampaignCategoryConnection cat
				ON cat.cCampaignId=g.cId
				WHERE cCategoryId = @categoryId
				AND cLocationId = @locationId
				AND cLanguageId = @languageId
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 2)
			BEGIN
				SELECT g.*
				FROM fnGetAllCampaigns(1, @Locationid, @Languageid) g
				INNER JOIN tblCampaignLocationConnection loc
				ON loc.cCampaignId=g.cId
				INNER JOIN tblCampaignLanguageConnection lang
				ON lang.cCampaignId=g.cId
				WHERE cLocationId = @locationId
				AND cLanguageId = @languageId
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 3)
			BEGIN
				SELECT g.*
				FROM fnGetAllCampaigns(1, @Locationid, @Languageid) g
				INNER JOIN tblCampaignLocationConnection loc
				ON loc.cCampaignId=g.cId
				INNER JOIN tblCampaignLanguageConnection lang
				ON lang.cCampaignId=g.cId
				INNER JOIN tblCampaignCategoryConnection cat
				ON cat.cCampaignId=g.cId
				WHERE cCategoryId = @categoryId
				AND cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cActive = 1
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 4)
			BEGIN
				SELECT g.*
				FROM fnGetAllCampaigns(1, @Locationid, @Languageid) g
				INNER JOIN tblCampaignLocationConnection loc
				ON loc.cCampaignId=g.cId
				INNER JOIN tblCampaignLanguageConnection lang
				ON lang.cCampaignId=g.cId
				WHERE cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cActive = 1
				AND (cStartDate <= getDate() OR cStartDate IS NULL)
				AND (cEndDate >= getDate() OR cEndDate IS NULL)
				ORDER BY cCreatedDate ASC
			END
			SELECT @status = @@ERROR
		END
