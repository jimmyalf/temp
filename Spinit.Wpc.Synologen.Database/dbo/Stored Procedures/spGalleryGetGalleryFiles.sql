CREATE PROCEDURE spGalleryGetGalleryFiles
					@type INT,
					@galleryId INT,
					@categoryId INT,
					@owner INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @sortType INT

			IF (@type = 0)
			BEGIN
				
				SELECT @sortType=cSortType FROM tblGallery
				WHERE cId = @galleryId
				
				IF @sortType = 1
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					AND tblGalleryFiles.cVisible = 1
					ORDER BY tblBaseFile.cCreatedDate
				END
				
				IF @sortType = 2
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					AND tblGalleryFiles.cVisible = 1
					ORDER BY tblBaseFile.cDescription
				END
				
				IF @sortType = 3
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					AND tblGalleryFiles.cVisible = 1
					ORDER BY tblGalleryFiles.cOrder
				END
				 
			END
			
			IF (@type = 1)
			BEGIN
	
				SELECT @sortType=cSortType FROM tblGallery
				WHERE cId = @galleryId
				
				IF @sortType = 1
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					ORDER BY tblBaseFile.cCreatedDate
				END
				
				IF @sortType = 2
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					ORDER BY tblBaseFile.cDescription
				END
				
				IF @sortType = 3
				BEGIN
					SELECT *, tblGalleryFiles.cGalleryId,tblGallery.cOwner
					FROM tblBaseFile
					INNER JOIN tblGalleryFiles
					ON tblBaseFile.cId=tblGalleryFiles.cFileId
					INNER JOIN tblGallery
					ON tblGallery.cId=tblGalleryFiles.cGalleryId
					WHERE tblGalleryFiles.cGalleryId = @galleryId
					ORDER BY tblGalleryFiles.cOrder
				END
			END
			
			IF (@type = 2)
			BEGIN
				SELECT * , tblGalleryFiles.cGalleryId,tblGallery.cOwner
				FROM tblBaseFile
				INNER JOIN tblGalleryFiles
				ON tblBaseFile.cId=tblGalleryFiles.cFileId
				INNER JOIN tblGallery
				ON tblGallery.cId=tblGalleryFiles.cGalleryId
				WHERE tblGalleryFiles.cVisible = 1
				ORDER BY tblBaseFile.cCreatedDate DESC
			END
			
			IF (@type = 3)
			BEGIN
				SELECT * , tblGalleryFiles.cGalleryId,tblGallery.cOwner
				FROM tblBaseFile
				INNER JOIN tblGalleryFiles
				ON tblBaseFile.cId=tblGalleryFiles.cFileId
				INNER JOIN tblGallery
				ON tblGallery.cId=tblGalleryFiles.cGalleryId
				WHERE tblGalleryFiles.cGalleryId IN (SELECT cGalleryId FROM tblGalleryCategoryConnection 
														WHERE cCategoryId = @categoryId )
				AND tblGalleryFiles.cVisible = 1
				ORDER BY tblBaseFile.cCreatedDate DESC
			END
			
			IF (@type = 4)
			BEGIN
				SELECT * , tblGalleryFiles.cGalleryId,tblGallery.cOwner
				FROM tblBaseFile
				INNER JOIN tblGalleryFiles
				ON tblBaseFile.cId=tblGalleryFiles.cFileId
				INNER JOIN tblGallery
				ON tblGallery.cId=tblGalleryFiles.cGalleryId
				WHERE tblGallery.cOwner = @owner
				AND tblGalleryFiles.cVisible = 1
				ORDER BY tblBaseFile.cCreatedDate DESC
			END
			
			IF (@type = 5)
			BEGIN
				SELECT * , tblGalleryFiles.cGalleryId,tblGallery.cOwner
				FROM tblBaseFile
				INNER JOIN tblGalleryFiles
				ON tblBaseFile.cId=tblGalleryFiles.cFileId
				INNER JOIN tblGallery
				ON tblGallery.cId=tblGalleryFiles.cGalleryId
				WHERE tblGalleryFiles.cGalleryId IN (SELECT cGalleryId FROM tblGalleryCategoryConnection 
														WHERE cCategoryId = @categoryId )
				AND tblGallery.cOwner = @owner	
				AND tblGalleryFiles.cVisible = 1									
				ORDER BY tblBaseFile.cCreatedDate DESC
			END
			
			
			
			SELECT @status = @@ERROR
		END
