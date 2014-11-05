
CREATE PROCEDURE spCampaignGetCategories
					@type INT,
					@categoryid INT,
					@campaignid INT,
					@locationId INT,
					@languageid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	fnGetAllCampaignCategories(@locationid,
					@languageid)
					WHERE cCategoryId = @categoryId								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	fnGetCampaignCategories(@campaignid,
					@locationid, @languageid)
						
				END
			IF (@type = 2)
				BEGIN				
					SELECT	*
					FROM	fnGetAllCampaignCategories(@locationid,
					@languageid)
						
				END
			SELECT @status = @@ERROR
		END
