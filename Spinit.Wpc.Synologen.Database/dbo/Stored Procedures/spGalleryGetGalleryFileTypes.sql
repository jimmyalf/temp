CREATE PROCEDURE spGalleryGetGalleryFileTypes
					@type INT,
					@filetypeId INT,
					@galleryId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
				SELECT * 
				FROM tblGalleryFileType				
				WHERE cId = @filetypeId
			END
			
			IF (@type = 1)
			BEGIN
				SELECT * 
				FROM tblGalleryFileType
				INNER JOIN tblGalleryFileTypeConnection
				ON tblGalleryFileType.cId=tblGalleryFileTypeConnection.cFileTypeId
				WHERE tblGalleryFileTypeConnection.cGalleryId = @galleryId
				ORDER BY cDescription
			END
			
			IF (@type = 2)
			BEGIN
				SELECT * 
				FROM tblGalleryFileType
				ORDER BY cDescription
			END
			
			
			SELECT @status = @@ERROR
		END
