CREATE PROCEDURE spCourseGetCities
					@type INT,
					@cityId INT,
					@status INT OUTPUT
					
AS
	BEGIN
		IF (@type = 0)
		BEGIN
			SELECT * FROM tblCourseCity
			WHERE cId = @cityId
		END
		
		IF (@type = 1)
		BEGIN
			SELECT	*
			FROM	tblCourseCity
		END
			

		SELECT @status = @@ERROR
	END
