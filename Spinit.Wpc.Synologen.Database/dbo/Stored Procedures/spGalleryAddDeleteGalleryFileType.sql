/*

*/
create PROCEDURE spGalleryAddDeleteGalleryFileType
					@type INT,
					@galleryId INT,					
					@fileTypeId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblGalleryFileTypeConnection
				(cGalleryId, cFileTypeId)
			VALUES 
				(@galleryId, @fileTypeId)
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblGalleryFileTypeConnection
			WHERE cGalleryId = @galleryId
			AND cFileTypeId = @fileTypeId
			
		END

		SELECT @status = @@ERROR
