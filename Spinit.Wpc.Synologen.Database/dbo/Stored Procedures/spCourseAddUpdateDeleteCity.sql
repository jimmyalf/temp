CREATE PROCEDURE spCourseAddUpdateDeleteCity
					@action INT,
					@id INT OUTPUT,
					@city NVARCHAR (255)= '',
					@status INT OUTPUT
AS
		BEGIN TRANSACTION CITY_ADD_UPDATE_DELETE
		DECLARE @myError INT
		SELECT @myError = 0
		IF (@action = 0) -- Create
		BEGIN
			INSERT INTO tblCourseCity (cCity) VALUES (@city)			
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN 
			UPDATE tblCourseCity
			SET cCity = @city WHERE cId = @id
		END
		IF (@action = 2) -- Delete
		BEGIN
			SELECT * FROM tblCourse WHERE cCityId = @id
			IF (@@ROWCOUNT = 0)
			BEGIN
				DELETE FROM tblCourseCity WHERE cId = @id
			END
			ELSE
			BEGIN
				SELECT @myError = -1
			END
		END

		SELECT @status = @@ERROR
		IF (@myError <> 0) BEGIN SELECT @status=-1 END
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION CITY_ADD_UPDATE_DELETE
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION CITY_ADD_UPDATE_DELETE
		END
