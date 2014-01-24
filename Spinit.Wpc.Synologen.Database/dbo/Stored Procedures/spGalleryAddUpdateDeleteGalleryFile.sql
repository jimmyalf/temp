/*

*/
create PROCEDURE spGalleryAddUpdateDeleteGalleryFile
					@type INT,
					@galleryId INT,					
					@fileId INT,
					@visible BIT = 1,
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
		
		IF (@type = 1) -- update
		BEGIN
			UPDATE tblGalleryFiles
			SET cVisible = @visible
			WHERE cGalleryId = @galleryId
			AND cFileId = @fileId
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblGalleryFiles
			WHERE cGalleryId = @galleryId
			AND cFileId = @fileId
			
		END

		SELECT @status = @@ERROR
