
CREATE PROCEDURE spCampaignAddDeleteCampaignCategory
					@type INT,
					@campaignId INT,					
					@categoryId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblCampaignCategoryConnection
				(cCampaignId, cCategoryId)
			VALUES 
				(@campaignId, @categoryId)
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblCampaignCategoryConnection
			WHERE cCampaignId = @campaignId
			AND cCategoryId = @categoryId
			
		END

		SELECT @status = @@ERROR

