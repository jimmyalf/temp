CREATE PROCEDURE spGalleryGetCategories
					@type INT,
					@categoryid INT,
					@galleryid INT,
					@locationId INT,
					@languageid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	sfGalleryGetAllGalleryCategories(@locationid,
					@languageid)
					WHERE cCategoryId = @categoryId								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	sfGalleryGetGalleryCategories(@galleryid,
					@locationid, @languageid)
					--ORDER BY cOrderId ASC	
				END
			IF (@type = 2)
				BEGIN				
					SELECT	*
					FROM	sfGalleryGetAllGalleryCategories(@locationid,
					@languageid)
					ORDER BY cOrderId ASC	
				END
			SELECT @status = @@ERROR
		END
