
CREATE PROCEDURE spCampaignAddDeleteCampaignLanguage
					@type INT,
					@campaignId INT,					
					@languageId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblCampaignLanguageConnection
				(cCampaignId, cLanguageId)
			VALUES 
				(@campaignId, @languageId)
		END
		
		IF (@type = 1) -- delete
		BEGIN
			DELETE FROM tblCampaignLanguageConnection
			WHERE cCampaignId = @campaignId
			AND cLanguageId = @languageId
			
		END

		SELECT @status = @@ERROR

