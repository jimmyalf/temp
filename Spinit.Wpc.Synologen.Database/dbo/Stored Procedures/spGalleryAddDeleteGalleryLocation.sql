
CREATE PROCEDURE spGalleryAddDeleteGalleryLocation
					@type INT,
					@galleryId INT,					
					@locationId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblGalleryLocationConnection
				(cGalleryId, cLocationId)
			VALUES 
				(@galleryId, @locationId)
		END
		
		IF (@type = 1) -- delete
		BEGIN
			DELETE FROM tblGalleryLocationConnection
			WHERE cGalleryId = @galleryId
			AND cLocationId = @locationId
			
			
		END

		SELECT @status = @@ERROR

