create PROCEDURE spCourseCreCourseFile
					@courseId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		INSERT INTO	tblCourseFileConnection
			(cCourseId, cFileId)
		VALUES
			(@courseId, @fleId)
			
		SELECT @status = @@ERROR
