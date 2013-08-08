create PROCEDURE spBaseUpdateFile
					@id INT,
					@name NVARCHAR (256),
					@contentInfo NVARCHAR (256),
					@keyWords NVARCHAR (256),
					@description NVARCHAR (256),
					@changedBy NVARCHAR (100),
					@status INT OUTPUT		
	AS
	BEGIN 
		DECLARE	@internContentInfo NVARCHAR (256),
				@internKeyWords NVARCHAR (256),
				@internDescription NVARCHAR (256)
			
		DECLARE get_file CURSOR FOR
			SELECT	cContentInfo,
					cKeyWords,
					cDescription
			FROM	tblBaseFile
			WHERE	cId = @id
			
		OPEN get_file
		FETCH NEXT FROM get_file INTO	@internContentInfo,
										@internKeyWords,
										@internDescription
		
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_file
				DEALLOCATE get_file
				
				SET @status = -1
				RETURN
			END
					
		IF (@contentInfo IS NOT NULL)
			BEGIN
				IF (LEN (@contentInfo) = 0)
					BEGIN
						SET @internContentInfo = NULL
					END
				ELSE
					BEGIN
						SET @internContentInfo = @contentInfo
					END
			END
			
		IF (@keyWords IS NOT NULL)
			BEGIN
				IF (LEN (@keyWords) = 0)
					BEGIN
						SET @internKeyWords = NULL
					END
				ELSE
					BEGIN
						SET @internKeyWords = @keyWords
					END
			END
			
		IF (@description IS NOT NULL)
			BEGIN
				IF (LEN (@description) = 0)
					BEGIN
						SET @internDescription = NULL
					END
				ELSE
					BEGIN
						SET @internDescription = @description
					END
			END
			
		IF (@name IS NOT NULL)
			BEGIN	
				UPDATE	tblBaseFile			
				SET		cName = @name,
						cContentInfo = @internContentinfo,
						cKeyWords = @internKeywords,
						cDescription = @internDescription,
						cChangedBy = @changedBy,
						cChangedDate = GETDATE ()
				WHERE	cId = @id
			END	
		ELSE 
			BEGIN
				UPDATE	tblBaseFile			
				SET		cContentInfo = @internContentinfo,
						cKeyWords = @internKeywords,
						cDescription = @internDescription,
						cChangedBy = @changedBy,
						cChangedDate = GETDATE ()
				WHERE	cId = @id
			END
			
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END