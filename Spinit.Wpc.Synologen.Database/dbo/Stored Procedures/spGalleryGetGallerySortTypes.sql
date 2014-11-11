CREATE PROCEDURE spGalleryGetGallerySortTypes
					@type INT,
					@sorttypeId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
				SELECT * 
				FROM tblGallerySortType				
				WHERE cId = @sorttypeId
			END
			
			IF (@type = 1)
			BEGIN
				SELECT * 
				FROM tblGallerySortType
				ORDER BY cDescription
			END
			
			
			SELECT @status = @@ERROR
		END
