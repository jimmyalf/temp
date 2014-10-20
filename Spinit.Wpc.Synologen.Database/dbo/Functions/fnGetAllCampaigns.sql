CREATE FUNCTION fnGetAllCampaigns (@type INT, 
@locationid INT, 
@languageid INT)
RETURNS @campaignList table
(
	cId int not null,
	cName nvarchar (50),
	cHeading nvarchar(512),
	cDescription nvarchar(512),
	cCampaignSpot int,
	cSpotHeight int,
	cSpotWidth int,
	cCampaignType int,
	cThumbsRows int,
	cThumbsColumns int,
	cThumbsHeight int,
	cThumbsWidth int,
	cListRowsPerPage int,
	cActive bit  ,
	cStartDate datetime NULL ,
	cEndDate datetime NULL ,
	cCreatedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cCreatedDate datetime NULL ,
	cEditedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cEditedDate datetime NULL ,
	cApprovedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cApprovedDate datetime NULL ,
	cLockedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cLockedDate datetime NULL  	
)	
AS
BEGIN
	DECLARE @defaultLanguageId INT
	DECLARE @inserted INT
	IF (@locationid > 0)
	BEGIN
		SELECT @defaultLanguageId = cLanguageId
		FROM tblBaseLocationsLanguages
		WHERE cIsDefault = 1 AND cLocationId = @locationid
	END

	DECLARE @id INT
	DECLARE getAll CURSOR LOCAL FOR
	SELECT cCampaignId FROM tblCampaignLocationConnection
	WHERE cLocationId = @locationid
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id
	IF (@type = 0)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @campaignList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @languageid),
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @languageid),
				g.cCampaignSpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cCampaignType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cStartDate, g.cEndDate,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblCampaign g
				INNER JOIN tblCampaignLanguageConnection glc 
						ON glc.cCampaignId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @languageid
				SET @inserted = @@ROWCOUNT
			IF (@inserted = 0)
			BEGIN 
				INSERT INTO @campaignList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @defaultLanguageId),
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @defaultLanguageId),
				g.cCampaignSpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cCampaignType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cStartDate, g.cEndDate,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblCampaign g
				INNER JOIN tblCampaignLanguageConnection glc 
						ON glc.cCampaignId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @defaultLanguageId
				SET @inserted = @@ROWCOUNT
				
			END	
			IF (@inserted = 0)
			BEGIN 												
				DECLARE @langId INT
				SELECT TOP 1 @langId = cLanguageId
				FROM	tblCampaign g
				INNER JOIN tblCampaignLanguageConnection glc 
					ON glc.cCampaignId = g.cId
				WHERE g.cId = @id

				INSERT INTO @campaignList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @langId),
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @langId),
				g.cCampaignSpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cCampaignType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cStartDate, g.cEndDate,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblCampaign g
				INNER JOIN tblCampaignLanguageConnection glc 
						ON glc.cCampaignId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @langId
			END
			FETCH NEXT FROM getAll INTO @id
		END
	END
	IF (@type = 1)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @campaignList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @languageid),
				(SELECT cString FROM tblCampaignLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @languageid),
				g.cCampaignSpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cCampaignType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cStartDate, g.cEndDate,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblCampaign g
				INNER JOIN tblCampaignLanguageConnection glc 
						ON glc.cCampaignId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @languageid
			FETCH NEXT FROM getAll INTO @id
		END	
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
