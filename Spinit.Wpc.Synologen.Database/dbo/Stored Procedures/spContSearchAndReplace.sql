create PROCEDURE spContSearchAndReplace 
	@FindString    NVARCHAR(100)
    ,@ReplaceString NVARCHAR(100)
	AS
	BEGIN
		SET NOCOUNT ON

		DECLARE @TextPointer VARBINARY(16) 
		DECLARE @DeleteLength INT 
		DECLARE @OffSet INT 


		SET @DeleteLength = LEN(@FindString) 
		SET @OffSet = 0
		SET @FindString = '%' + @FindString + '%'
		DECLARE @id INT
		DECLARE getContent CURSOR LOCAL FOR
			SELECT cId
			FROM tblContPage
			WHERE PATINDEX(@FindString, cContent) <> 0

		OPEN getContent
		FETCH NEXT FROM getContent INTO @id

		WHILE (@@FETCH_STATUS <> -1)
		BEGIN

			  WHILE (SELECT COUNT(*)
						 FROM tblContPage
						WHERE (PATINDEX(@FindString, cContent) <> 0) AND (cId = @id) ) > 0
				BEGIN 
					SELECT @TextPointer = TEXTPTR(cContent), 
					@OffSet = PATINDEX(@FindString, cContent) - 1
					FROM tblContPage
					WHERE cId = @id

					UPDATETEXT tblContPage.cContent
						@TextPointer
						@OffSet
						@DeleteLength
						@ReplaceString
				END
			
			FETCH NEXT FROM getContent INTO @id
		END
		CLOSE getContent
		DEALLOCATE getContent
		SET NOCOUNT OFF
	END
