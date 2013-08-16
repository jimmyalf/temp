create PROCEDURE spCourseRemCourseFile
					@courseId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblCourseFileConnection
		WHERE		cCourseId = @courseId
			AND		cFileId = @fleId
					
		SELECT @status = @@ERROR
