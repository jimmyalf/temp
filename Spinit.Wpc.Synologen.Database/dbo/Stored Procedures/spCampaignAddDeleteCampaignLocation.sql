
CREATE PROCEDURE spCampaignAddDeleteCampaignLocation
					@type INT,
					@campaignId INT,					
					@locationId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblCampaignLocationConnection
				(cCampaignId, cLocationId)
			VALUES 
				(@campaignId, @locationId)
		END
		
		IF (@type = 1) -- delete
		BEGIN
			DELETE FROM tblCampaignLocationConnection
			WHERE cCampaignId = @campaignId
			AND cLocationId = @locationId
			
			
		END

		SELECT @status = @@ERROR

