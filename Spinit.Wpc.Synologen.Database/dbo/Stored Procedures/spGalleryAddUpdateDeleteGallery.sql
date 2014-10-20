CREATE PROCEDURE spGalleryAddUpdateDeleteGallery
					@type INT,
					@languageid INT = 0,
					@id INT OUTPUT,
					@owner INT = 0,			
					@name NVARCHAR(50) = '',
					@heading NVARCHAR(255) = '',
					@description NVARCHAR(255) = '',
					@gallerySpot INT,
					@spotHeight INT,
					@spotWidth INT,
					@galleryType INT,
					@thumbsRows INT,
					@thumbsColumns INT,
					@thumbsHeight INT,
					@thumbsWidth INT,
					@listRowsPerPage INT,
					@defaultDirectory INT = 0,
					@sortType INT = 0,
					@active BIT = 1,
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
		BEGIN TRANSACTION ADD_UPDATE_DELETE_GALLERY
		
		IF (@type = 0) -- create
		BEGIN	
			SET @headingStringId = 1
			SELECT TOP 1 @headingStringId = cId + 1
			FROM tblGalleryLanguageStrings
			ORDER BY cId DESC
					
			INSERT INTO tblGalleryLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@headingStringId, @languageid, @heading)
				
			SET @descriptionStringId = 1
			SELECT TOP 1 @descriptionStringId = cId + 1
			FROM tblGalleryLanguageStrings
			ORDER BY cId DESC
					
			INSERT INTO tblGalleryLanguageStrings
				(cId, cLngId, cString)
			VALUES
				(@descriptionStringId, @languageid, @description)
		
			INSERT INTO tblGallery
				(cOwner, cName, cHeadingStringId, cDescriptionStringId, cGallerySpot, 
				cSpotHeight, cSpotWidth, cGalleryType, cThumbsRows,
				cThumbsColumns, cThumbsHeight, cThumbsWidth, cListRowsPerPage,
				cDefaultDirectory, cSortType,
				cActive, cCreatedBy, cCreatedDate, cEditedBy, cEditedDate,
				cApprovedBy, cApprovedDate, cLockedBy, cLockedDate)
			VALUES 
				(@owner, @name, @headingStringId, @descriptionStringId, @gallerySpot, 
				@spotHeight, @spotWidth, @galleryType, @thumbsRows,
				@thumbsColumns, @thumbsHeight, @thumbsWidth,
				@listRowsPerPage, @defaultDirectory, @sortType,
				@active, @createdBy, getDate() , null, null,
				@approvedBy, @approvedDate, @lockedBy, @lockedDate)
					
			SELECT @id = @@IDENTITY
		END

		IF (@type = 1) --update
		BEGIN
			
			SELECT @headingStringId = cHeadingStringId, @descriptionStringId = cDescriptionStringId 
			FROM tblGallery
			WHERE cId = @id
			
			UPDATE tblGalleryLanguageStrings
			SET cString = @heading
			WHERE cId = @headingStringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblGalleryLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@headingStringId, @languageid, @heading)
			END
			
			UPDATE tblGalleryLanguageStrings
			SET cString = @description
			WHERE cId = @descriptionStringId AND cLngId = @languageid
			
			IF (@@ROWCOUNT = 0)
			BEGIN
				INSERT INTO tblGalleryLanguageStrings
					(cId, cLngId, cString)
				VALUES
					(@descriptionStringId, @languageid, @description)
			END
			
			UPDATE tblGallery
			SET 
				cName = @name,  
				cGallerySpot = @gallerySpot, 
				cSpotHeight = @spotHeight, 
				cSpotWidth = @spotWidth, 
				cGalleryType = @galleryType, 
				cThumbsRows = @thumbsRows,
				cThumbsColumns = @thumbsColumns, 
				cThumbsHeight = @thumbsHeight, 
				cThumbsWidth = @thumbsWidth, 
				cListRowsPerPage = @listRowsPerPage,
				cDefaultDirectory = @defaultDirectory,
				cSortType = @sortType,
				cActive = @active, 
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
			FROM tblGallery
			WHERE cId = @id	
			
			DELETE FROM tblGalleryLanguageStrings
			WHERE cId = @descriptionStringId OR cId = @headingStringId
			
			DELETE FROM tblGalleryCategoryConnection
			WHERE cGalleryId = @id
			
			DELETE FROM tblGalleryLanguageConnection
			WHERE cGalleryId = @id
			
			DELETE FROM tblGalleryLocationConnection
			WHERE cGalleryId = @id
			
			DELETE FROM tblGalleryFiles
			WHERE cGalleryId = @id
			
			DELETE FROM tblGalleryFileTypeConnection
			WHERE cGalleryId = @id
			
			DELETE FROM tblGalleryGroupConnection
			WHERE cGalleryId = @id
			
			DELETE FROM tblGallery
			WHERE cId = @id
			
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_GALLERY
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_GALLERY
			END
