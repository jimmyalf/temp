/*

*/

CREATE PROCEDURE spGalleryGetGalleriesDynamic
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
	AS
	
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	DECLARE @Trunc INT
	SET @Trunc = 20
	SELECT @sql=
			'SELECT 
				gallery.cId,
				gallery.cName,
				gallery.cHeading,
				gallery.cDescription,
				gallery.cGallerySpot,
				gallery.cSpotHeight,
				gallery.cSpotWidth,
				gallery.cGalleryType,
				gallery.cThumbsRows,
				gallery.cThumbsColumns,
				gallery.cThumbsHeight,
				gallery.cThumbsWidth,
				gallery.cListRowsPerPage,
				gallery.cActive,
				gallery.cCreatedBy,
				gallery.cCreatedDate,
				gallery.cEditedBy,
				gallery.cEditedDate,
				gallery.cApprovedBy,
				gallery.cApprovedDate,
				gallery.cLockedBy,
				gallery.cLockedDate
				FROM sfGalleryGetAllGalleries(1, @xLocationid, @xLanguageid) gallery'
	IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@LanguageId IS NOT NULL AND @LanguageId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblGalleryLocationConnection ON gallery.cId = tblGallerylocationConnection.cGalleryId 
		INNER JOIN tblGalleryLanguageConnection ON gallery.cId = tblGalleryLanguageConnection.cGalleryId 
		INNER JOIN tblGalleryCategoryConnection ON gallery.cId = tblGalleryCategoryConnection.cGalleryId 
		WHERE tblGalleryLocationConnection.cLocationid = @xLocationId AND 
		tblGalleryLanguageConnection.cLanguageId = @xLanguageId AND 
		tblGalleryCategoryConnection.cCategoryId = @xCategoryId'
	END				   
	ELSE IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@LanguageId IS NOT NULL AND @LanguageId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblGalleryLocationConnection ON gallery.cId = tblGallerylocationConnection.cGalleryId 
		INNER JOIN tblGalleryLanguageConnection ON gallery.cId = tblGalleryLanguageConnection.cGalleryId 
		WHERE tblGalleryLocationConnection.cLocationid = @xLocationId AND 
		tblGalleryLanguageConnection.cLanguageId = @xLanguageId'
	END				
	ELSE IF ((@LocationId IS NOT NULL AND @LocationId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblGalleryLocationConnection ON gallery.cId = tblGallerylocationConnection.cGalleryId 
		INNER JOIN tblGalleryCategoryConnection ON gallery.cId = tblGalleryCategoryConnection.cGalleryId 
		WHERE tblGalleryLocationConnection.cLocationid = @xLocationId AND 
		tblGalleryCategoryConnection.cCategoryId = @xCategoryId'
	END
	ELSE IF ((@LanguageId IS NOT NULL AND @LanguageId > 0) AND
		(@CategoryId IS NOT NULL AND @CategoryId > 0))
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblGalleryLanguageConnection ON gallery.cId = tblGalleryLanguageConnection.cGalleryId 
		INNER JOIN tblGalleryCategoryConnection ON gallery.cId = tblGalleryCategoryConnection.cGalleryId 
		WHERE tblGalleryLanguageConnection.cLanguageId = @xLanguageId AND 
		tblGalleryCategoryConnection.cCategoryId = @xCategoryId'
	END
	ELSE IF (@LocationId IS NOT NULL AND @LocationId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblGalleryLocationConnection ON gallery.cId = tblGallerylocationConnection.cGalleryId 
		WHERE tblGalleryLocationConnection.cLocationid = @xLocationId'
	END
	ELSE IF (@LanguageId IS NOT NULL AND @LanguageId > 0)
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblGalleryLanguageConnection ON gallery.cId = tblGalleryLanguageConnection.cGalleryId 
		WHERE tblGalleryLanguageConnection.cLanguageId = @xLanguageId'
	END
	ELSE IF (@CategoryId IS NOT NULL AND @CategoryId > 0)
	BEGIN
		SELECT @sql = @sql + 
		'INNER JOIN tblGalleryCategoryConnection ON gallery.cId = tblGalleryCategoryConnection.cGalleryId 
		WHERE tblGalleryCategoryConnection.cCategoryid = @xCategoryId'
	END
	ELSE
	BEGIN					
		SELECT @sql = @sql + ' WHERE 1=1'
	END
	
    IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
    BEGIN
		SELECT @sql = @sql + ' AND (cHeading LIKE ''%''+@xSearchString+
		''%''OR cName LIKE ''%''+@xSearchString+''%''+
		''%''OR cDescription LIKE ''%''+@xSearchString+''%'')'
	END
	IF (@OrderBy IS NOT NULL)
	BEGIN
		
		SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
		
	END
	SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xLocationId INT, @xLanguageId INT, @xCategoryId INT'
						
	EXEC sp_executesql @sql,
						@paramlist,                                 
						@SearchString,
						@OrderBy,
						@LocationId,
						@LanguageId,
						@CategoryId
