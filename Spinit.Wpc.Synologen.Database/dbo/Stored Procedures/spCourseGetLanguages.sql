CREATE PROCEDURE spCourseGetLanguages
					@type INT,
					@courseId INT=-1,
					@status INT OUTPUT
	AS
		IF (@type = 1)
		BEGIN
			SELECT * FROM tblBaseLanguages		
		END		
		IF (@type = 6)
		BEGIN
			SELECT * FROM tblCourseLanguageConnection 
			INNER JOIN tblBaseLanguages 
				ON tblCourseLanguageConnection.cLanguageId=tblBaseLanguages.cId
			WHERE tblCourseLanguageConnection.cCourseId = @courseId		
		END
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
