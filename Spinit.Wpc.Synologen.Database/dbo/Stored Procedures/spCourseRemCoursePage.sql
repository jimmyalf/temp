create PROCEDURE spCourseRemCoursePage
					@courseId INT,
					@pageId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblCoursePageConnection
		WHERE		cCourseId = @courseId
			AND		cPageId = @pageId
			
		SELECT @status = @@ERROR
