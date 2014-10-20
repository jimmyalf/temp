create PROCEDURE spCourseCreCoursePage
					@courseId INT,
					@pageId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @dummy INT
			
			DECLARE get_pge CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContPage
				WHERE	cId = @pageId
								
			OPEN get_pge
			FETCH NEXT FROM get_pge INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_pge
					DEALLOCATE get_pge
					
					SELECT @status = 0
					RETURN
				END
				
			CLOSE get_pge
			DEALLOCATE get_pge
						
			INSERT INTO tblCoursePageConnection
				(cCourseId, cPageId)
			VALUES
				(@courseId, @pageId)
				
			SELECT @status = @@ERROR
		END
