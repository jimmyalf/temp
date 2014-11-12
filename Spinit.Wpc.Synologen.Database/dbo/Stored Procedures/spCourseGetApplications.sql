CREATE PROCEDURE spCourseGetApplications
					@type INT,
					@applicationId INT,
					@courseId INT,
					@status INT OUTPUT			
AS
	BEGIN
		IF (@type = 0) --Specific application
		BEGIN
			SELECT * FROM tblCourseApplication
			WHERE cId = @applicationId
		END
		
		IF (@type = 1) --All
		BEGIN
			SELECT * FROM tblCourseApplication
		END

		IF (@type = 7) --All for specific course
		BEGIN
			SELECT * FROM tblCourseApplication 
			WHERE cCourseId = @courseId
		END

		SELECT @status = @@ERROR
	END
