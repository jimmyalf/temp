
CREATE PROCEDURE spCampaignGetFileCategories
					@type INT,
					@categoryid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	tblCampaignFileCategory
					WHERE cId = @categoryId								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	tblCampaignFileCategory
					ORDER BY cDescription ASC	
				END
			SELECT @status = @@ERROR
		END
