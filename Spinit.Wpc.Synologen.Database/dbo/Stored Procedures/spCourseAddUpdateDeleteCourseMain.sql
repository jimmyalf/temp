create PROCEDURE spCourseAddUpdateDeleteCourseMain
					@action INT,
					@id INT OUTPUT,
					@name NVARCHAR (50)= '',
					@description NTEXT= '',
					@detail NTEXT= '',
					@status INT OUTPUT
AS
		BEGIN TRANSACTION COURSEMAIN_ADD_UPDATE_DELETE
		IF (@action = 0) -- Create
		BEGIN
			INSERT INTO tblCourseMain (cName, cDescription, cDetail)
			VALUES				(@name, @description, @detail)			
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN 
			UPDATE tblCourseMain
			SET
				cName = @name,
				cDescription = @description,
				cDetail = @detail 
			WHERE cId = @id
		END
		IF (@action = 2) -- Delete
		BEGIN
			SELECT * FROM tblCourse WHERE cCourseMainId = @id
			IF (@@ROWCOUNT = 0)
			BEGIN
				--No delete tblCourseMain row if it has connected rows in tblCourse.
				DELETE FROM tblCourseMainCategoryConnection WHERE cCourseMainId = @id
				DELETE FROM tblCourseMain WHERE cId = @id
			END
			ELSE
			BEGIN
				SELECT @status = -1
			END
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION COURSEMAIN_ADD_UPDATE_DELETE
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION COURSEMAIN_ADD_UPDATE_DELETE
		END
