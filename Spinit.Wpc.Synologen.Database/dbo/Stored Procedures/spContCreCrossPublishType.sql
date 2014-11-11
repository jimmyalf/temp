
CREATE PROCEDURE spContCreCrossPublishType
					@name NVARCHAR (50),
					@description NVARCHAR (256),
					@id INT OUTPUT,
					@status INT OUTPUT
	AS
		BEGIN
			INSERT INTO tblContCrossPublishType
				(cName, cDescription)
			VALUES
				(@name, @description)
				
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @id = 0
					SELECT @status = @@ERROR
				END
			ELSE
				BEGIN
					SELECT @id = @@IDENTITY
					SELECT @status = @@ERROR
				END
		END
