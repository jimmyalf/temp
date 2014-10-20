
CREATE PROCEDURE spGalleryAddDeleteGalleryCategory
					@type INT,
					@galleryId INT,					
					@categoryId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblGalleryCategoryConnection
				(cGalleryId, cCategoryId)
			VALUES 
				(@galleryId, @categoryId)
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblGalleryCategoryConnection
			WHERE cGalleryId = @galleryId
			AND cCategoryId = @categoryId
			
		END

		SELECT @status = @@ERROR

