create PROCEDURE spGalleryMoveGalleryFile
					@direction INT,
					@galleryId INT,
					@fileId INT,
					@status INT OUTPUT
	AS
	
		DECLARE @order INT,
			@max INT
							
						
		DECLARE get_galleryfile CURSOR LOCAL FOR
			SELECT	cOrder
			FROM	tblGalleryFiles
			WHERE	cGalleryId = @galleryId
			AND		cFileId = @fileId
			
		OPEN get_galleryfile
		FETCH NEXT FROM get_galleryfile INTO @order
										
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_galleryfile
				DEALLOCATE get_galleryfile
				
				SET @status = -1
				RETURN
			END
			
		CLOSE get_galleryfile
		DEALLOCATE get_galleryfile
		
		/*Get max*/
		SELECT	@max = MAX (cOrder)
		FROM	tblGalleryFiles
		WHERE cGalleryId = @galleryId
		AND cOrder IS NOT NULL
		
		
		IF (((@order = 1) AND (@direction = 0))
			OR ((@order = @max) AND (@direction = 1)))
			BEGIN
				SET @status = 0
				RETURN
			END
	
	
	IF @direction = 0 /*Up*/
	
	BEGIN
		BEGIN TRANSACTION MOVE_GALLERYFILE_UP
		UPDATE	tblGalleryFiles
		SET		cOrder = -1
		WHERE	cGalleryId = @galleryId
		AND		cFileId = @fileId
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_UP
				RETURN
			END
		
		UPDATE	tblGalleryFiles
		SET		cOrder = @order
		WHERE	cGalleryId = @galleryId
		AND		cOrder = @order - 1
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_UP
				RETURN
			END
		
		UPDATE	tblGalleryFiles
		SET		cOrder = @order - 1
		WHERE	cGalleryId = @galleryId
		AND		cFileId = @fileId
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_UP
				RETURN
			END
		
		COMMIT TRANSACTION MOVE_GALLERYFILE_UP
	END
	
	IF @direction = 1 /*Down*/
	BEGIN
		BEGIN TRANSACTION MOVE_GALLERYFILE_DOWN
		
		UPDATE	tblGalleryFiles
		SET		cOrder = -1
		WHERE	cGalleryId = @galleryId
		AND		cFileId = @fileId
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_DOWN
				RETURN
			END
		
		UPDATE	tblGalleryFiles
		SET		cOrder = @order
		WHERE	cGalleryId = @galleryId
		AND		cOrder = @order + 1
			
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_DOWN
				RETURN
			END
				
		UPDATE	tblGalleryFiles
		SET		cOrder = @order + 1
		WHERE	cGalleryId = @galleryId
		AND		cFileId = @fileId
		
		IF (@@ERROR <> 0)
			BEGIN
				SET @status = @@ERROR
				ROLLBACK TRANSACTION MOVE_GALLERYFILE_DOWN
				RETURN
			END
			
		COMMIT TRANSACTION MOVE_GALLERYFILE_DOWN
		
		
	END
	
	IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
