CREATE FUNCTION fnGetAllGalleries (@type INT, 
@locationid INT, 
@languageid INT)
RETURNS @galleryList table
(
	cId int not null,
	cName nvarchar (50),
	cHeading nvarchar(512),
	cDescription nvarchar(512),
	cGallerySpot int,
	cSpotHeight int,
	cSpotWidth int,
	cGalleryType int,
	cThumbsRows int,
	cThumbsColumns int,
	cThumbsHeight int,
	cThumbsWidth int,
	cListRowsPerPage int,
	cActive bit  ,
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
	SELECT cGalleryId FROM tblGalleryLocationConnection
	WHERE cLocationId = @locationid
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id
	IF (@type = 0)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @galleryList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @languageid),
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @languageid),
				g.cGallerySpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cGalleryType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblGallery g
				INNER JOIN tblGalleryLanguageConnection glc 
						ON glc.cGalleryId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @languageid
				SET @inserted = @@ROWCOUNT
			IF (@inserted = 0)
			BEGIN 
				INSERT INTO @galleryList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @defaultLanguageId),
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @defaultLanguageId),
				g.cGallerySpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cGalleryType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblGallery g
				INNER JOIN tblGalleryLanguageConnection glc 
						ON glc.cGalleryId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @defaultLanguageId
				SET @inserted = @@ROWCOUNT
				
			END	
			IF (@inserted = 0)
			BEGIN 												
				DECLARE @langId INT
				SELECT TOP 1 @langId = cLanguageId
				FROM	tblGallery g
				INNER JOIN tblGalleryLanguageConnection glc 
					ON glc.cGalleryId = g.cId
				WHERE g.cId = @id

				INSERT INTO @galleryList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @langId),
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @langId),
				g.cGallerySpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cGalleryType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblGallery g
				INNER JOIN tblGalleryLanguageConnection glc 
						ON glc.cGalleryId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @langId
			END
			FETCH NEXT FROM getAll INTO @id
		END
	END
	IF (@type = 1)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @galleryList
				SELECT	g.cId,
				g.cName,
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cHeadingStringId AND cLngId = @languageid),
				(SELECT cString FROM tblGalleryLanguageStrings WHERE cId = cDescriptionStringId AND cLngId = @languageid),
				g.cGallerySpot,
				g.cSpotHeight,
				g.cSpotWidth,
				g.cGalleryType,
				g.cThumbsRows,
				g.cThumbsColumns,
				g.cThumbsHeight,
				g.cThumbsWidth,
				g.cListRowsPerPage,
				g.cActive,
				g.cCreatedBy, g.cCreatedDate, g.cEditedBy, g.cEditedDate,
				g.cApprovedBy, g.cApprovedDate, g.cLockedBy, g.cLockedDate
				FROM	tblGallery g
				INNER JOIN tblGalleryLanguageConnection glc 
						ON glc.cGalleryId = g.cId
				WHERE g.cId = @id AND glc.cLanguageId = @languageid
			FETCH NEXT FROM getAll INTO @id
		END	
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
