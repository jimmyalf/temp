
CREATE PROCEDURE spGalleryAddDeleteGalleryLanguage
					@type INT,
					@galleryId INT,					
					@languageId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblGalleryLanguageConnection
				(cGalleryId, cLanguageId)
			VALUES 
				(@galleryId, @languageId)
		END
		
		IF (@type = 1) -- delete
		BEGIN
			DELETE FROM tblGalleryLanguageConnection
			WHERE cGalleryId = @galleryId
			AND cLanguageId = @languageId
			
		END

		SELECT @status = @@ERROR

