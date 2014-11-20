CREATE PROCEDURE spCampaignAddUpdateDeleteCampaign
					@type INT,
					@languageid INT = 0,
					@id INT OUTPUT,					
					@name NVARCHAR(50) = '',
					@heading NVARCHAR(255) = '',
					@description NVARCHAR(255) = '',
					@campaignSpot INT,
					@spotHeight INT,
					@spotWidth INT,
					@campaignType INT,
					@thumbsRows INT,
					@thumbsColumns INT,
					@thumbsHeight INT,
					@thumbsWidth INT,
					@listRowsPerPage INT,
					@active BIT = 1,
					@startDate DATETIME = '',
					@endDate DATETIME = '',
					@createdDate DATETIME = '',
					@createdBy NVARCHAR(100) = '',
					@editedBy NVARCHAR(100) = '',
					@approvedBy NVARCHAR(100) = '',
					@approvedDate DATETIME = '',
					@lockedBy NVARCHAR(100) = '',
					@lockedDate DATETIME = '',
					@status INT OUTPUT
	AS
		DECLARE @headingStringId INT
		DECLARE @descriptionStringId INT
		BEGIN TRANSACTION ADD_UPDATE_DELETE_CAMPAIGN
		
		IF (@type = 0) -- create
		BEGIN	
			SET @headingStringId = 1
			SELECT TOP 1 @headingStringId = cId + 1
			FROM tblCampaignLanguageStrings
			ORDER BY cId DESC
					
			INSERT INTO tblCampaignLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@headingStringId, @languageid, @heading)
				
			SET @descriptionStringId = 1
			SELECT TOP 1 @descriptionStringId = cId + 1
			FROM tblCampaignLanguageStrings
			ORDER BY cId DESC
					
			INSERT INTO tblCampaignLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@descriptionStringId, @languageid, @description)
		
			INSERT INTO tblCampaign
				(cName, cHeadingStringId, cDescriptionStringId, cCampaignSpot, 
				cSpotHeight, cSpotWidth, cCampaignType, cThumbsRows,
				cThumbsColumns, cThumbsHeight, cThumbsWidth, cListRowsPerPage,
				cActive, cStartDate, cEndDate, cCreatedBy, cCreatedDate, cEditedBy, cEditedDate,
				cApprovedBy, cApprovedDate, cLockedBy, cLockedDate)
			VALUES 
				(@name, @headingStringId, @descriptionStringId, @campaignSpot, 
				@spotHeight, @spotWidth, @campaignType, @thumbsRows,
				@thumbsColumns, @thumbsHeight, @thumbsWidth,
				@listRowsPerPage, @active, @startDate, @endDate, @createdBy, getDate() , null, null,
				@approvedBy, @approvedDate, @lockedBy, @lockedDate)
					
			SELECT @id = @@IDENTITY
		END

		IF (@type = 1) --update
		BEGIN
			
			SELECT @headingStringId = cHeadingStringId, @descriptionStringId = cDescriptionStringId 
			FROM tblCampaign
			WHERE cId = @id
			
			UPDATE tblCampaignLanguageStrings
			SET cString = @heading
			WHERE cId = @headingStringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblCampaignLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@headingStringId, @languageid, @heading)
			END
			
			UPDATE tblCampaignLanguageStrings
			SET cString = @description
			WHERE cId = @descriptionStringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblCampaignLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@descriptionStringId, @languageid, @description)
			END
			
			UPDATE tblCampaign
			SET 
				cName = @name,  
				cCampaignSpot = @campaignSpot, 
				cSpotHeight = @spotHeight, 
				cSpotWidth = @spotWidth, 
				cCampaignType = @campaignType, 
				cThumbsRows = @thumbsRows,
				cThumbsColumns = @thumbsColumns, 
				cThumbsHeight = @thumbsHeight, 
				cThumbsWidth = @thumbsWidth, 
				cListRowsPerPage = @listRowsPerPage,
				cActive = @active, 
				cStartDate = @startDate,
				cEndDate = @endDate,
				cCreatedBy = @createdBy,
				cCreatedDate = @createdDate,
				cEditedBy = @editedBy,
				cEditedDate = getDate(),
				cApprovedBy = @approvedBy,
				cApprovedDate = @approvedDate,
				cLockedBy = @lockedBy,
				cLockedDate = @lockedDate
			WHERE
				cId = @id				
		END
		
		IF (@type = 2) -- delete
		BEGIN
			SELECT @descriptionStringId = cDescriptionStringId,
					@headingStringId = cHeadingStringId 
			FROM tblCampaign
			WHERE cId = @id	
			
			DELETE FROM tblCampaignLanguageStrings
			WHERE cId = @descriptionStringId OR cId = @headingStringId
			
			DELETE FROM tblCampaignCategoryConnection
			WHERE cCampaignId = @id
			
			DELETE FROM tblCampaignLanguageConnection
			WHERE cCampaignId = @id
			
			DELETE FROM tblCampaignLocationConnection
			WHERE cCampaignId = @id
			
			DELETE FROM tblCampaign
			WHERE cId = @id
			
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_CAMPAIGN
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_CAMPAIGN
			END
