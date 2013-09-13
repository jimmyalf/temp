/*

*/
create PROCEDURE spGalleryAddDeleteGalleryFile
					@type INT,
					@galleryId INT,					
					@fileId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			DECLARE @max INT
			
			SELECT @max = Max(cOrder)
			FROM tblGalleryFiles
			WHERE cGalleryId = @galleryId
			AND cOrder IS NOT NULL

			IF @max IS NULL
			BEGIN
				SET @max = 0
			END
			
			SET @max = @max + 1
		
			INSERT INTO tblGalleryFiles
				(cGalleryId, cFileId,cOrder)
			VALUES 
				(@galleryId, @fileId, @max)
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblGalleryFiles
			WHERE cGalleryId = @galleryId
			AND cFileId = @fileId
			
		END

		SELECT @status = @@ERROR
