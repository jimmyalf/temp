/*

*/
create PROCEDURE spGalleryAddDeleteGalleryGroup
					@type INT,
					@galleryId INT,					
					@groupId INT,
					@status INT OUTPUT
	AS
		
		
		IF (@type = 0) -- add
		BEGIN	
			INSERT INTO tblGalleryGroupConnection
				(cGalleryId, cGroupId)
			VALUES 
				(@galleryId, @groupId)
		END
		
		IF (@type = 2) -- delete
		BEGIN
			DELETE FROM tblGalleryGroupConnection
			WHERE cGalleryId = @galleryId
			AND cGroupId = @groupId
			
		END

		SELECT @status = @@ERROR
